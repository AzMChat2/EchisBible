// These global variables are necessary to avoid losing scope when
//setting the balloon timeout and for inter-object communication
var currentBalloonClass;
var balloonIsVisible;
var balloonIsSticky;
var balloonInvisibleSelects;
var balloonIsSuppressed;
var tooltipIsSuppressed;

//////////////////////////////////////////////////////////////////////////
// This is constructor that is called to initialize the Balloon object  //
//////////////////////////////////////////////////////////////////////////
var Balloon = function () {

  this.suppress = true;

  // Cursor tracking halts after one of these vertical
  // or horizontal thresholds are reached
  this.stopTrackingX = 100;
  this.stopTrackingY = 50;

  // Track the cursor every time the mouse moves
  document.onmousemove = this.setActiveCoordinates;

  // scrolling aborts unsticky balloons
  document.onscroll    = Balloon.prototype.hideTooltip;

  // make balloons go away if the page is unloading or waiting
  // to unload.
  window.onbeforeunload = function(){
    Balloon.prototype.hideTooltip(1);
    balloonIsSuppressed = true;
  };

    this.fontColor = 'black';
    this.fontFamily = 'Arial, sans-serif';
    this.fontSize = '12pt';
    this.minWidth = 100;
    this.maxWidth = 400;
    this.delayTime = 500;
    this.stem = true;
    this.images = imagePath;
    this.ieImage = 'balloon.png';
    this.balloonImage = 'balloon.png';
    this.upLeftStem = 'up_left.png';
    this.downLeftStem = 'down_left.png';
    this.upRightStem = 'up_right.png';
    this.downRightStem = 'down_right.png';
    this.closeButton = 'close.png';
    this.closeButtonWidth = 16;
    this.trackCursor = false;
    this.shadow = 20;
    this.padding = 10;
    this.stemHeight = 32;
    this.stemOverlap = 3;
    this.vOffset = 1;
    this.hOffset = 1;
    this.opacity = 95;
    this.allowFade = true;
    this.fadeIn = 500;
    this.fadeOut = 200;
    this.configured = true;

  return this;
}

Region = function(t,r,b,l) {
	this.top=t;
	this[1]=t;
	this.right=r;
	this.bottom=b;
	this.left=l;
	this[0]=l;
}

Region.prototype.intersect = function(region) {
	var t=Math.max(this.top,region.top);
	var r=Math.min(this.right,region.right);
	var b=Math.min(this.bottom,region.bottom);
	var l=Math.max(this.left,region.left);
	if(b >= t && r >= l) {
		return new Region(t,r,b,l);
	} else {
		return null;
	}
}

//////////////////////////////////////////////////////////////////////////
// This is the function that is called on mouseover.  It has a built-in //
// delay time to avoid balloons popping up on rapid mouseover events    //
//////////////////////////////////////////////////////////////////////////
Balloon.prototype.showTooltip = function(evt,caption,sticky,width,height) {
  
  // Awful IE bug, page load aborts if the balloon is fired
  // before the page is fully loaded.
  if (document.readyState.match(/complete/i)) {
    this.suppress = false;
  }
  
  // All balloons have been suppressed, go no further
  if (this.suppress || balloonIsSuppressed) {
    return false;
  }

  // Check for mouseover (vs. mousedown or click)
  var mouseOver = evt.type.match('mouseover','i');  

  this.fadeOK = this.allowFade;

  // Ignore repeated firing of mouseover->mouseout events on 
  // the same element (Safari)
  var el = this.getEventTarget(evt);
  this.firingElement = el;

  // A new sticky balloon can erase an old one
  if (sticky) this.hideTooltip(1);

  // attach a mouseout event handler to the target element
  el.onmouseout = function() { 
    var override = balloonIsSticky && !balloonIsVisible;
    Balloon.prototype.hideTooltip(override)
  }
  
  this.hideTooltip();
  this.currentHelpText = caption;

  // no contents? abort.
  if (!this.currentHelpText) return false;

  this.width  = width;
  this.height = height;
  this.actualWidth = null;

  // make sure old balloons are removed
  this.cleanup();

  // Put the balloon contents and images into a visible (but offscreen)
  // element so they will be preloaded and have a layout to 
  // calculate the balloon dimensions
  this.container = document.createElement('div');
  this.container.id = 'container';
  document.body.appendChild(this.container);
  this.setStyle(this.container,'position','absolute');
  this.setStyle(this.container,'top',-8888);
  this.setStyle(this.container,'font-family',this.fontFamily);
  this.setStyle(this.container,'font-size',this.fontSize);

  // protect escaped '&'
  this.currentHelpText = this.currentHelpText.replace(/\&amp;/g, '&amp;amp');
  this.container.innerHTML = unescape(this.currentHelpText);
  
  // make sure balloon image path is complete
  if (this.images) {

    // main background image
    this.balloonImage  = this.balloonImage  ? this.images +'/'+ this.balloonImage  : false;
    this.ieImage       = this.ieImage       ? this.images +'/'+ this.ieImage       : false;

    // optional stems
    this.upLeftStem    = this.upLeftStem    ? this.images +'/'+ this.upLeftStem    : false;
    this.upRightStem   = this.upRightStem   ? this.images +'/'+ this.upRightStem   : false;
    this.downLeftStem  = this.downLeftStem  ? this.images +'/'+ this.downLeftStem  : false;
    this.downRightStem = this.downRightStem ? this.images +'/'+ this.downRightStem : false;

    this.closeButton   = this.closeButton   ? this.images +'/'+ this.closeButton   : false;

    this.images        = false;
  }

  // The PNG alpha channels (shadow transparency) are not 
  // handled properly by IE < 6.  Also, if opacity is set to
  // < 1 (translucent balloons), any version of IE does not
  // handle the image properly.
  // Google chrome is a bit dodgey too
  // If there is an IE image provided, use that instead.
  if (this.ieImage && (this.isIE() || this.isChrome())) {
    if (this.isOldIE() || this.opacity || this.allowFade) {    
      this.balloonImage = this.ieImage;
    }
  }

  // preload balloon images 
  if (!this.preloadedImages) {
    var images = new Array(this.balloonImage, this.closeButton);
    if (this.ieImage) {
      images.push(this.ieImage);
    }
    if (this.stem) {
      images.push(this.upLeftStem,this.upRightStem,this.downLeftStem,this.downRightStem);
    }
    var len = images.length;
    for (var i=0;i<len;i++) {
      if ( images[i] ) {
        this.preload(images[i]);
      }
    }
    this.preloadedImages = true;
  }

  currentBalloonClass = this;

  // Remember which event started this
  this.currentEvent = evt;

  // prevent interaction with gbrowse drag and drop
  evt.cancelBubble  = true;

  this.timeoutTooltip = window.setTimeout(this.doShowTooltip, this.delayTime);
  this.pending = true;
}


// Preload the balloon background images
Balloon.prototype.preload = function(src) {
  var i = new Image;
  i.src = src;

  // append to the DOM tree so the images have a layout,
  // then remove.
  this.setStyle(i,'position','absolute');
  this.setStyle(i,'top',-8000);
  document.body.appendChild(i);
  document.body.removeChild(i);
}


/////////////////////////////////////////////////////////////////////
// Tooltip rendering function
/////////////////////////////////////////////////////////////////////
Balloon.prototype.doShowTooltip = function() {
  var self = currentBalloonClass;

  // Stop firing if a balloon is already being displayed
  if (balloonIsVisible) return false;  

  if (!self.parent) {
    if (self.parentID) {
      self.parent = document.getElementById(self.parentID);
    }
    else {
      self.parent = document.body;
    }
    self.xOffset = self.getLoc(self.parent, 'x1');
    self.yOffset = self.getLoc(self.parent, 'y1');
  }

  // a short delay time might cause some intereference
  // with fading
  window.clearTimeout(self.timeoutFade);
  self.setStyle('balloon','display','none');

  // create the balloon object
  var balloon = self.makeBalloon();

  // window dimensions
  var pageWidth   = document.body.clientWidth; 
  var pageCen     = Math.round(pageWidth/2);
  var pageHeight  = document.body.clientHeight; 
  var pageLeft    = document.body.scrollLeft; 
  var pageTop     = document.body.scrollTop; 
  var pageMid     = pageTop + Math.round(pageHeight/2);
  self.pageBottom = pageTop + pageHeight;
  self.pageTop    = pageTop;
  self.pageLeft   = pageLeft;
  self.pageRight  = pageLeft + pageWidth;

  // balloon orientation
  var vOrient = self.activeTop > pageMid ? 'up' : 'down';
  var hOrient = self.activeRight > pageCen ? 'left' : 'right';
  
  // get the preloaded balloon contents
  var helpText = self.container.innerHTML;
  self.actualWidth = self.getLoc(self.container,'width') + 10;
  self.parent.removeChild(self.container);
  var wrapper = document.createElement('div');
  wrapper.id = 'contentWrapper';
  self.contents.appendChild(wrapper);
  wrapper.innerHTML = helpText;

  // how and where to draw the balloon
  self.setBalloonStyle(vOrient,hOrient,pageWidth,pageLeft);

  balloonIsVisible = true;
  self.pending = false;  

  // in IE < 7, hide <select> elements
  self.showHide();

  self.startX = self.activeLeft;
  self.startY = self.activeTop;

  self.fade(0,self.opacity,self.fadeIn);
}

// use a fresh object every time to make sure style 
// is not polluted
Balloon.prototype.makeBalloon = function() {
  var self = currentBalloonClass;

  var balloon = document.getElementById('balloon');
  if (balloon) {
    self.cleanup();
  }

  balloon = document.createElement('div');
  balloon.setAttribute('id','balloon');
  self.parent.appendChild(balloon);
  self.activeBalloon = balloon;

  self.parts = new Array();
  var parts = new Array('contents','topRight','bottomRight','bottomLeft');
  for (var i=0;i<parts.length;i++) {
    var child = document.createElement('div');
    child.setAttribute('id',parts[i]);
    balloon.appendChild(child);
    if (parts[i] == 'contents') self.contents = child;
    self.parts.push(child);
  }
  //self.parts.push(balloon);

  if (self.displayTime)  {
    self.timeoutAutoClose = window.setTimeout(this.hideTooltip,self.displayTime);
  }
  return balloon;
}

Balloon.prototype.setBalloonStyle = function(vOrient,hOrient,pageWidth,pageLeft) {
  var self = currentBalloonClass;
  var balloon = self.activeBalloon;

  if (typeof(self.shadow) != 'number') self.shadow = 0;
  if (!self.stem) self.stemHeight = 0;

  var fullPadding   = self.padding + self.shadow;
  var insidePadding = self.padding;
  var outerWidth    = self.actualWidth + fullPadding;
  var innerWidth    = self.actualWidth;

  self.setStyle(balloon,'position','absolute');
  self.setStyle(balloon,'top',-9999);
  self.setStyle(balloon,'z-index',1000000);

  if (self.height) {
    self.setStyle('contentWrapper','height',self.height-fullPadding);
  }

  if (self.width) {
    self.setStyle(balloon,'width',self.width);  
    innerWidth = self.width - fullPadding;
    if (balloonIsSticky) {
      innerWidth -= self.closeButtonWidth;
    }
    self.setStyle('contentWrapper','width',innerWidth);
  }
  else {
    self.setStyle(balloon,'width',outerWidth);
    self.setStyle('contentWrapper','width',innerWidth);
  }

  // not too big...
  if (!self.width && self.maxWidth && outerWidth > self.maxWidth) {
    self.setStyle(balloon,'width',self.maxWidth);
    self.setStyle('contentWrapper','width',self.maxWidth - fullPadding);
  }
  // not too small...
  if (!self.width && self.minWidth && outerWidth < self.minWidth) {
    self.setStyle(balloon,'width',self.minWidth);
    self.setStyle('contentWrapper','width',self.minWidth - fullPadding);
  }

  self.setStyle('contents','z-index',2);
  self.setStyle('contents','color',self.fontColor);
  self.setStyle('contents','font-family',self.fontFamily);
  self.setStyle('contents','font-size',self.fontSize);
  self.setStyle('contents','background','url('+self.balloonImage+') top left no-repeat');
  self.setStyle('contents','padding-top',fullPadding);
  self.setStyle('contents','padding-left',fullPadding);

  self.setStyle('bottomRight','background','url('+self.balloonImage+') bottom right no-repeat');
  self.setStyle('bottomRight','position','absolute');
  self.setStyle('bottomRight','right',0-fullPadding);
  self.setStyle('bottomRight','bottom',0-fullPadding);
  self.setStyle('bottomRight','height',fullPadding);
  self.setStyle('bottomRight','width',fullPadding);
  self.setStyle('bottomRight','z-index',-1);

  self.setStyle('topRight','background','url('+self.balloonImage+') top right no-repeat');
  self.setStyle('topRight','position','absolute');
  self.setStyle('topRight','right',0-fullPadding);
  self.setStyle('topRight','top',0);
  self.setStyle('topRight','width',fullPadding);

  self.setStyle('bottomLeft','background','url('+self.balloonImage+') bottom left no-repeat');
  self.setStyle('bottomLeft','position','absolute');
  self.setStyle('bottomLeft','left',0);
  self.setStyle('bottomLeft','bottom',0-fullPadding);
  self.setStyle('bottomLeft','height',fullPadding);
  self.setStyle('bottomLeft','z-index',-1);

  if (this.stem) {
    var stem = document.createElement('img');
    self.setStyle(stem,'position','absolute');
    balloon.appendChild(stem);
 
    if (vOrient == 'up' && hOrient == 'left') {  
      stem.src = self.upLeftStem;
      var height = self.stemHeight + insidePadding - self.stemOverlap;
      self.setStyle(stem,'bottom',0-height);
      self.setStyle(stem,'right',0);             
    }
    else if (vOrient == 'down' && hOrient == 'left') {
      stem.src = self.downLeftStem;
      var height = self.stemHeight - (self.shadow + self.stemOverlap);
      self.setStyle(stem,'top',0-height);
      self.setStyle(stem,'right',0);
    }
    else if (vOrient == 'up' && hOrient == 'right') {
      stem.src = self.upRightStem;
      var height = self.stemHeight + insidePadding - self.stemOverlap;
      self.setStyle(stem,'bottom',0-height);
      self.setStyle(stem,'left',self.shadow);
    }
    else if (vOrient == 'down' && hOrient == 'right') {
      stem.src = self.downRightStem;
      var height = self.stemHeight - (self.shadow + self.stemOverlap);
      self.setStyle(stem,'top',0-height);
      self.setStyle(stem,'left',self.shadow);
    }
    if (self.fadeOK && self.isIE()) {
      self.parts.push(stem);
    }
  }

  if (self.allowFade) {
    self.setOpacity(1);
  }
  else if (self.opacity) {
    self.setOpacity(self.opacity);
  }

  // flip left or right, as required
  if (hOrient == 'left') {
    var pageWidth = self.pageRight - self.pageLeft;
    var activeRight = pageWidth - self.activeLeft;
    self.setStyle(balloon,'right',activeRight);
  }
  else {
    var activeLeft = self.activeRight - self.xOffset;
    self.setStyle(balloon,'left',activeLeft);
  }

  // oversized contents? Scrollbars for sticky balloons, clipped for non-sticky
  var overflow = balloonIsSticky ? 'auto' : 'hidden';
  self.setStyle('contentWrapper','overflow',overflow);

  // a bit of room for the closebutton
  if (balloonIsSticky) {
    self.setStyle('contentWrapper','margin-right',self.closeButtonWidth);
  }

  // Make sure the balloon is not offscreen horizontally.
  // We handle vertical sanity checking later, after the final
  // layout is set.
  var balloonLeft   = self.getLoc(balloon,'x1');
  var balloonRight  = self.getLoc(balloon,'x2');
  var scrollBar     = 20;

  if (hOrient == 'right' && balloonRight > (self.pageRight - fullPadding)) {
    var width = (self.pageRight - balloonLeft) - fullPadding - scrollBar;
    self.setStyle(balloon,'width',width);
    self.setStyle('contentWrapper','width',width-fullPadding);
  }
  else if (hOrient == 'left' && balloonLeft < (self.pageLeft + fullPadding)) {
    var width = (balloonRight - self.pageLeft) - fullPadding;
    self.setStyle(balloon,'width',width);
    self.setStyle('contentWrapper','width',width-fullPadding);
  }

  // Get the width/height for the right and bottom outlines
  var balloonWidth  = self.getLoc(balloon,'width');
  var balloonHeight = self.getLoc(balloon,'height');

  // IE7 quirk -- look for unwanted overlap cause by an off by 1px error
  var vOverlap = self.isOverlap('topRight','bottomRight');
  var hOverlap = self.isOverlap('bottomLeft','bottomRight');
  if (vOverlap) {
    self.setStyle('topRight','height',balloonHeight-vOverlap[1]);
  }
  if (hOverlap) {
    self.setStyle('bottomLeft','width',balloonWidth-hOverlap[0]);
  }

  // vertical position of the balloon
  if (vOrient == 'up') {
    var activeTop = self.activeTop - balloonHeight;
    self.setStyle(balloon,'top',activeTop);
  }
  else {
    var activeTop = self.activeBottom;
    self.setStyle(balloon,'top',activeTop);
  }

  // Make sure the balloon is vertically contained in the window
  var balloonTop    = activeTop; // self.getLoc(balloon,'y1');
  var balloonBottom = self.height ? balloonTop + self.height : balloonTop + balloonHeight; // self.getLoc(balloon,'y2'); 
  var deltaTop      = balloonTop < self.pageTop ? self.pageTop - balloonTop : 0;
  var deltaBottom   = balloonBottom > self.pageBottom ? balloonBottom - self.pageBottom : 0;
	
  if (vOrient == 'up' && deltaTop) {
    var newHeight = balloonHeight - deltaTop;
    if (newHeight > (self.padding *2 )) {
      self.setStyle('contentWrapper','height',newHeight-fullPadding);
      self.setStyle(balloon,'top',self.pageTop+self.padding);
      self.setStyle(balloon,'height',newHeight);
    }
  }
  
  if (vOrient == 'down' && deltaBottom) {
    var newHeight = balloonHeight - deltaBottom - scrollBar;
    if (newHeight > (self.padding * 2) + scrollBar) {
      self.setStyle('contentWrapper','height',newHeight-fullPadding);
      self.setStyle(balloon,'height',newHeight);
    }
  }

  // If we have an iframe, make sure it fits properly
  var iframe = balloon.getElementsByTagName('iframe');
  if (iframe[0]) {
    iframe = iframe[0];
    var w = self.getLoc('contentWrapper','width');
    if (balloonIsSticky && !this.isIE()) {
      w -= self.closeButtonWidth;
    }
    var h = self.getLoc('contentWrapper','height');
    self.setStyle(iframe,'width',w);
    self.setStyle(iframe,'height',h);
    self.setStyle('contentWrapper','overflow','hidden');
  }

  // Make edges match the main balloon body
  self.setStyle('topRight','height', self.getLoc(balloon,'height'));
  self.setStyle('bottomLeft','width', self.getLoc(balloon,'width'));

  self.hOrient = hOrient;
  self.vOrient = vOrient;
}


// Fade method adapted from an example on 
// http://brainerror.net/scripts/javascript/blendtrans/
Balloon.prototype.fade = function(opacStart, opacEnd, millisec) {
  var self = currentBalloonClass || new Balloon;
  if (!millisec || !self.allowFade) {
    return false;
  }

  opacEnd = opacEnd || 100;

  //speed for each frame
  var speed = Math.round(millisec / 100);
  var timer = 0;
  for(o = opacStart; o <= opacEnd; o++) {
    self.timeoutFade = setTimeout('Balloon.prototype.setOpacity(' + o + ')', (timer * speed));
    timer++;
  }
}

Balloon.prototype.setOpacity = function(opc) {
  var self = currentBalloonClass;
  if (!self || !opc) return false;

  var o = parseFloat(opc/100);

  // opacity handled differently for IE
  var parts = self.isIE() ? self.parts : [self.activeBalloon];

  var len = parts.length;
  for (var i=0;i<len;i++) {
    self.doOpacity(o,opc,parts[i]);
  }
}

Balloon.prototype.doOpacity = function(op,opc,el) {
  var self = currentBalloonClass;
  if (!el) return false;

  // CSS standards-compliant browsers!
  self.setStyle(el,'opacity',op);

  // old IE
  self.setStyle(el,'filter','alpha(opacity='+opc+')');

}

Balloon.prototype.hideTooltip = function(override) { 
  // some browsers pass the event object == we don't want it
  if (override && typeof override == 'object') override = false;
  if (balloonIsSticky && !override) return false;

  var self = currentBalloonClass;
  Balloon.prototype.showHide(1);
  Balloon.prototype.cleanup();

  if (self) {
    window.clearTimeout(self.timeoutTooltip);
    window.clearTimeout(self.timeoutFade);
    window.clearTimeout(self.timeoutAutoClose);
    if (balloonIsSticky) {
      self.currentElement = null;
    }
    self.startX = 0;
    self.startY = 0;
  }

  balloonIsVisible = false;
  balloonIsSticky  = false;
}

// Garbage collection
Balloon.prototype.cleanup = function() {
  var self = currentBalloonClass;
  var body;
  if (self) {
    body = self.parent   ? self.parent 
         : self.parentID ? document.getElementById(self.parentID) || document.body
         : document.body;
  }
  else {
    body = document.body;
  }

  var bubble = document.getElementById('balloon');
  var close  = document.getElementById('closeButton');
  var cont   = document.getElementById('container');
  if (bubble) { body.removeChild(bubble) } 
  if (close)  { body.removeChild(close)  }
  if (cont)   { body.removeChild(cont)   }
}


// this function is meant to be called externally to clear
// any open balloons
hideAllTooltips = function() {
  var self = currentBalloonClass;
  if (!self) return;
  window.clearTimeout(self.timeoutTooltip);
  if (self.activeBalloon) self.setStyle(self.activeBalloon,'display','none');
  balloonIsVisible    = false;
  balloonIsSticky     = false;
  currentBalloonClass = null;
}


// Track the active mouseover coordinates
Balloon.prototype.setActiveCoordinates = function(evt) {
  var self = currentBalloonClass;
  if (!self) {
    return true;
  }
  var evt = evt || window.event || self.currentEvent;
  if (!evt) {
    return true;
  }

  // avoid silent NaN errors
  self.hOffset = self.hOffset || 1;
  self.vOffset = self.vOffset || 1;
  self.stemHeight = self.stem && self.stemHeight ? (self.stemHeight|| 0) : 0;

  var scrollTop  = 0;
  var scrollLeft = 0;

  var XY = self.eventXY(evt);
  adjustment   = self.hOffset < 20 ? 10 : 0;
  self.activeTop    = scrollTop  + XY[1] - adjustment - self.vOffset - self.stemHeight;
  self.activeLeft   = scrollLeft + XY[0] - adjustment - self.hOffset;
  self.activeRight  = scrollLeft + XY[0];
  self.activeBottom = scrollTop  + XY[1] + self.vOffset + 2*adjustment;

  // dynamic positioning but only if the balloon is not sticky
  // and cursor tracking is enabled
  if (balloonIsVisible && !balloonIsSticky && self.trackCursor) {
    var deltaX = Math.abs(self.activeLeft - self.startX);
    var deltaY = Math.abs(self.activeTop - self.startY);

    if (deltaX > self.stopTrackingX || deltaY > self.stopTrackingY) {
      self.hideTooltip();
    }
    else {
      var b = self.activeBalloon;
      var bwidth  = self.getLoc(b,'width');
      var bheight = self.getLoc(b,'height');
      var btop    = self.getLoc(b,'y1');
      var bleft   = self.getLoc(b,'x1');

      if (self.hOrient == 'right') {
        self.setStyle(b,'left',self.activeRight);
      }
      else if (self.hOrient == 'left') {
        self.setStyle(b,'right',null);
        var newLeft = self.activeLeft - bwidth;
        self.setStyle(b,'left',newLeft);
      }

      if (self.vOrient == 'up') {
        self.setStyle(b,'top',self.activeTop - bheight);
      }
      else if (self.vOrient == 'down') {
        self.setStyle(b,'top',self.activeBottom);
      }
    }
  }

  return true;
}

////
// event XY and getEventTarget Functions based on examples by Peter-Paul
// Koch http://www.quirksmode.org/js/events_properties.html
Balloon.prototype.eventXY = function(event) {
  var XY = new Array(2);
  var e = event || window.event;
  if (!e) {
    return false;
  }
  if (e.pageX || e.pageY) {
    XY[0] = e.pageX;
    XY[1] = e.pageY;
  }
  else if ( e.clientX || e.clientY ) {
    XY[0] = e.clientX + document.body.scrollLeft + document.documentElement.scrollLeft;
    XY[1] = e.clientY + document.body.scrollTop  + document.documentElement.scrollTop;
  }

  return XY;
}

Balloon.prototype.getEventTarget = function(event) {
  var targ;
  var e = event || window.event;
  if (e.target) targ = e.target;
  else if (e.srcElement) targ = e.srcElement;
  if (targ.nodeType == 3) targ = targ.parentNode; // Safari
  return targ;
}
////


Balloon.prototype.setStyle = function(el,att,val) {
  if (!el) { 
    return false;
  }
  if (typeof(el) != 'object') {
    el = document.getElementById(el);
  }
  if (!el) {
    return false;
  }
  
  if (val && att.match(/left|top|bottom|right|width|height|padding|margin/)) {
    val = new String(val);
    if (!val.match(/auto/)) {
      val += 'px';
    }
  }

  // z-index does not work as expected
  if (att == 'z-index') {
    if (el.style) {
      el.style.zIndex = parseInt(val);
    }
  }
  else {
    Balloon.prototype.setElementStyle(el, att, val);
  }
}

var propertyCache = {};
var patterns = {HYPHEN:/(-[a-z])/i,ROOT_TAG:/^body|html$/i};
var toCamel = function(property) {

	if (!patterns.HYPHEN.test(property)) return property;
	if (propertyCache[property]) return propertyCache[property];
	
	var converted = property;
	while(patterns.HYPHEN.exec(converted))
	{
		converted = converted.replace(RegExp.$1, RegExp.$1.substr(1).toUpperCase());
	}
	
	propertyCache[property] = converted;
	return converted;
}


Balloon.prototype.setElementStyle = function(el, property, val){
	
	property = toCamel(property);
	
	switch (property)
	{
		case 'opacity':
			el.style.filter = 'alpha(opacity=' + val * 100 + ')';
			if (!el.currentStyle || !el.currentStyle.hasLayout)
			{
				el.style.zoom=1;
			}
			break;
		case 'float':
			property = 'styleFloat';
		default:
			el.style[property] = val;
	}
}

getRegion = function(el)
{
	//var p = YAHOO.util.Dom.getXY(el);
	var p = getXY(el);
	var t = p[1];
	var r = p[0]+el.offsetWidth;
	var b = p[1]+el.offsetHeight;
	var l = p[0];
	
	return new Region(t,r,b,l);
}

getXY = function(ev) {
	return [getPageX(ev), getPageY(ev)];
}

getPageY = function(ev){
	var y = ev.pageY;
	if(!y && 0!==y) {
		y = ev.clientY || 0;
		y += document.body.scrollTop;
	}
	return y;
}

getPageX = function(ev) {
	var x = ev.pageX;
	if(!x && 0!==x)
	{
		x = ev.clientX || 0;
		x += document.body.scrollLeft;
	}
	return x;
}

// Uses YAHOO's region class for element coordinates
Balloon.prototype.getLoc = function(el, request) {
  //var region = YAHOO.util.Dom.getRegion(el);
	var region = getRegion(el);
	
  switch(request) {
    case ('y1') : return parseInt(region.top);
    case ('y2') : return parseInt(region.bottom);
    case ('x1') : return parseInt(region.left);
    case ('x2') : return parseInt(region.right);
    case ('width')  : return (parseInt(region.right)  - parseInt(region.left));
    case ('height') : return (parseInt(region.bottom) - parseInt(region.top));
    case ('region') : return region; 
  }

  return region;
}

// show/hide select elements in older IE
// plus user-defined elements
Balloon.prototype.showHide = function(visible) {
  var self = currentBalloonClass || new Balloon;

  // IE z-index bug fix (courtesy of Lincoln Stein)
  if (self.isOldIE()) {
    var balloonContents = document.getElementById('contentWrapper');
    if (!visible && balloonContents) {
      var balloonSelects = balloonContents.getElementsByTagName('select');
      var myHash = new Object();
      for (var i=0; i<balloonSelects.length; i++) {
        var id = balloonSelects[i].id || balloonSelects[i].name;
        myHash[id] = 1;
      }
      balloonInvisibleSelects = new Array();
      var allSelects = document.getElementsByTagName('select');
      for (var i=0; i<allSelects.length; i++) {
        var id = allSelects[i].id || allSelects[i].name;
        if (self.isOverlap(allSelects[i],self.activeBalloon) && !myHash[id]) {
          balloonInvisibleSelects.push(allSelects[i]);
          self.setStyle(allSelects[i],'visibility','hidden');
        }
      }
    }
    else if (balloonInvisibleSelects) {
      for (var i=0; i < balloonInvisibleSelects.length; i++) {
        var id = balloonInvisibleSelects[i].id || balloonInvisibleSelects[i].name;
        self.setStyle(balloonInvisibleSelects[i],'visibility','visible');
     }
     balloonInvisibleSelects = null;
    }
  }

  // show/hide any user-specified elements that overlap the balloon
  if (self.hide) {
    var display = visible ? 'inline' : 'none';
    for (var n=0;n<self.hide.length;n++) {
      if (self.isOverlap(self.activeBalloon,self.hide[n])) {
        self.setStyle(self.hide[n],'display',display);
      }
    }
  }
}

// Try to find overlap
Balloon.prototype.isOverlap = function(el1,el2) {
  if (!el1 || !el2) return false;
  var R1 = this.getLoc(el1,'region');
  var R2 = this.getLoc(el2,'region');
  if (!R1 || !R2) return false;
  var intersect = R1.intersect(R2);
  if (intersect) {
    // extent of overlap;
    intersect = new Array((intersect.right - intersect.left),(intersect.bottom - intersect.top));
  }
  return intersect;
}

// Coordinate-based test for the same element
Balloon.prototype.isSameElement = function(el1,el2) {
  if (!el1 || !el2) return false;
  var R1 = this.getLoc(el1,'region');
  var R2 = this.getLoc(el2,'region');
  var same = R1.contains(R2) && R2.contains(R1);
  return same ? true : false;
}

// test for internet explorer
Balloon.prototype.isIE = function() {
  return document.all && !window.opera;
}

// test for internet explorer (but not IE7)
Balloon.prototype.isOldIE = function() {
  if (navigator.appVersion.indexOf("MSIE") == -1) return false;
  var temp=navigator.appVersion.split("MSIE");
  return parseFloat(temp[1]) < 7;
}
