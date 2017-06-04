$('#newExercise').click(function () {
	$('.hiddenExercise')[0].classList.remove("hidden","hiddenExercise");
});

$('.cancel').click(function () {
	var parent = $(this).parent('div');
	parent.addClass('hidden');
	$('#exercisesToRemove').html =+$(this).closest('.id').html;
	
	var fieldsToClear = parent.children("input");
	for (var i = 0; i < fieldsToClear.length; i++) {
		var item = fieldsToClear.eq(i);
		if(!item.hasClass("id"))
			item.removeAttr('value');
	}
	parent.find('option:selected').removeAttr("selected");
});
