$('#back123').click(function (e) {
	e.preventDefault();
	alert(2);
});

$('#loadNew').click(function (e) {
	var workoutId = $('#workoutId').attr("value");
	$.get('/Workouts/AddExercise/' + workoutId, function (data) {
		$('#newExercise').html(data);
	});
});

window.onload = function () {
	var workoutId = $('#workoutId').attr("value");
	$.get('/Workouts/LoadExercises/' + workoutId, function (data) {
		$('#newExercise').html(data);
	});
};

//But your Ajax code is incomplete without success or error functions. // TODO <<