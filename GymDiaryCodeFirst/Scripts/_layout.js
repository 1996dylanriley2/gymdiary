// media query event handler
if (matchMedia) {
	var mq = window.matchMedia("(min-width: 768px)");
	mq.addListener(WidthChange);
	WidthChange(mq);
}
// media query change
function WidthChange(mq) {
	if (mq.matches) {
		// window width is at least 768px
		$('#sidebar-mobile').addClass('hide');
	} else {
		// window width is less than 767px
		
	}
}

$('#mobileMenuBtn').click(function () {
	$('#sidebar-mobile').toggleClass('hide');
	$('#body').toggleClass('shiftLeft');
});