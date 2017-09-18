$(document).ready(function () {
	var pageButton = $('.pagerLink');

	pageButton.click(function () {
		hideAllVirtualPages();

		var anchor = $(this).find('a');
		var id = anchor.attr('href');
		showItem(id);

	});

	function hideAllVirtualPages() {
		var $vPages = $('.virtual-page');
		$vPages.each(function () {
			hideClassIfNotHidden($(this));
		});
	}

	function hideClassIfNotHidden(el) {
		if (!el.hasClass('hide'))
			el.addClass('hide');
	}

	function showItem(id) {
		$(id).removeClass('hide');
	}
});
