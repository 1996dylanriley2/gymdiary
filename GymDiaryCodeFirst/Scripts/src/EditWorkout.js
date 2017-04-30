$('#loadNew').click(function (e) {
	e.preventDefault();
	$("#newExercise").append("<div></div>");
	$.get('/Workouts/AddExercise', function (data) {
		$('#newExercise :last-child').html(data);
	});
});