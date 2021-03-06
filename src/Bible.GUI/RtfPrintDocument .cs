using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Bible
{
	public sealed class RtfPrintDocument : PrintDocument
	{

		#region Win32 API Structs and Methods
		//Convert the unit used by the .NET framework (1/100 inch) 
		//and the unit used by Win32 API calls (twips 1/1440 inch)
		private const double anInch = 14.4;

		[StructLayout(LayoutKind.Sequential)]
		private struct RECT
		{
			public int Left;
			public int Top;
			public int Right;
			public int Bottom;
		}

		[StructLayout(LayoutKind.Sequential)]
		private struct CHARRANGE
		{
			public int cpMin;         //First character of range (0 for start of doc)
			public int cpMax;           //Last character of range (-1 for end of doc)
		}

		[StructLayout(LayoutKind.Sequential)]
		private struct FORMATRANGE
		{
			public IntPtr hdc;             //Actual DC to draw on
			public IntPtr hdcTarget;       //Target DC for determining text formatting
			public RECT rc;                //Region of the DC to draw to (in twips)
			public RECT rcPage;            //Region of the whole DC (page size) (in twips)
			public CHARRANGE chrg;         //Range of text to draw (see earlier declaration)
		}

		private const int WM_USER = 0x0400;
		private const int EM_FORMATRANGE = WM_USER + 57;

		#endregion

		private RichTextBox rtfBox = new RichTextBox();
		private int lastPosition;

		public string RtfText
		{
			get { return rtfBox.Rtf; }
			set { rtfBox.Rtf = value; }
		}

		protected override void OnBeginPrint(PrintEventArgs e)
		{
			lastPosition = 0;
			base.OnBeginPrint(e);
		}

		protected override void OnPrintPage(PrintPageEventArgs e)
		{
			e.HasMorePages = Print(e);
			base.OnPrintPage(e);
		}

		#region Print
		// Render the contents of the RichTextBox for printing
		//	Return the last character printed + 1 (printing start from this point for next page)
		private bool Print(PrintPageEventArgs e)
		{
			//Calculate the area to render and print
			RECT rectToPrint;
			rectToPrint.Top = (int)(e.MarginBounds.Top * anInch);
			rectToPrint.Bottom = (int)(e.MarginBounds.Bottom * anInch);
			rectToPrint.Left = (int)(e.MarginBounds.Left * anInch);
			rectToPrint.Right = (int)(e.MarginBounds.Right * anInch);

			//Calculate the size of the page
			RECT rectPage;
			rectPage.Top = (int)(e.PageBounds.Top * anInch);
			rectPage.Bottom = (int)(e.PageBounds.Bottom * anInch);
			rectPage.Left = (int)(e.PageBounds.Left * anInch);
			rectPage.Right = (int)(e.PageBounds.Right * anInch);

			IntPtr hdc = e.Graphics.GetHdc();

			FORMATRANGE fmtRange;
			fmtRange.chrg.cpMax = rtfBox.TextLength;				//Indicate character from to character to 
			fmtRange.chrg.cpMin = lastPosition;
			fmtRange.hdc = hdc;                    //Use the same DC for measuring and rendering
			fmtRange.hdcTarget = hdc;              //Point at printer hDC
			fmtRange.rc = rectToPrint;             //Indicate the area on page to print
			fmtRange.rcPage = rectPage;            //Indicate size of page

			IntPtr wparam = new IntPtr(1);

			//Get the pointer to the FORMATRANGE structure in memory
			IntPtr lparam = Marshal.AllocCoTaskMem(Marshal.SizeOf(fmtRange));
			Marshal.StructureToPtr(fmtRange, lparam, false);

			//Send the rendered data for printing 
			IntPtr res = NativeMethods.SendMessage(rtfBox.Handle, EM_FORMATRANGE, wparam, lparam);

			//Free the block of memory allocated
			Marshal.FreeCoTaskMem(lparam);

			//Release the device context handle obtained by a previous call
			e.Graphics.ReleaseHdc(hdc);

			//Return last + 1 character printer
			lastPosition = res.ToInt32();
			return rtfBox.TextLength > lastPosition;
		}
		#endregion

	}

	internal static class NativeMethods
	{
		[DllImport("USER32.dll")]
		public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
	}
}
