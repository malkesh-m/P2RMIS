
var ww = screen.width;

$(document).ready(function() {
	$(".respnav li a").each(function() {
		if ($(this).next().length > 0) {
			$(this).addClass("parent");
		};
	})
	
	$(".toggleMenu").click(function(e) {
		e.preventDefault();
		$(this).toggleClass("respactive");
		$(".respnav").toggle();
	});
	adjustMenu();
})

$(window).bind('resize orientationchange', function() {
	ww = screen.width;
	adjustMenu();
});

var adjustMenu = function() {
	if (ww < 768) {
		$(".toggleMenu").css("display", "inline-block");
		if (!$(".toggleMenu").hasClass("respactive")) {
			$(".respnav").hide();
		} else {
			$(".respnav").show();
		}
		$(".respnav li").unbind('mouseenter mouseleave');
		$(".respnav li a.parent").unbind('click').bind('click', function(e) {
			// must be attached to anchor element to prevent bubbling
			e.preventDefault();
			$(this).parent("li").toggleClass("hover");
		});
	} 
	else if (ww >= 768) {
		$(".toggleMenu").css("display", "none");
		$(".respnav").show();
		$(".respnav li").removeClass("hover");
		$(".respnav li a").unbind('click');
		$(".respnav li").unbind('mouseenter mouseleave').bind('mouseenter mouseleave', function() {
		 	// must be attached to li so that mouseleave is not triggered when hover over submenu
		 	$(this).toggleClass('hover');
		});
	}
}

