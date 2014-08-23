var chart1;
var chart2;
var teller = 10;
var dataTimer;
var displayTimer;
var selectedOption;


$(document).ready(function($) {
	fillQuestions();

});

function fillQuestions() {
	getQuestions();

}

function getQuestions() {
	var event = get('event');
	//$.getJSON(
	//		"http://localhost:8080/Voting/secure/eventResult/" + event
	//				+ "/questions")
	 $.getJSON( "http://voting.elasticbeanstalk.com/secure/eventResult/" + event + "/questions")
	.done(
			function(json) {
				questions = json.questions;
				var select = $("#selectBox");
				
				if (questions != null && questions.length > 0) {

					for (var i = 0; i < questions.length; i++) {
						var value = '<option value="' + questions[i].id + '">'
								+ questions[i].question + '</option>';

						select.append(value);
					}
				}

				var value = select.val();
				if (value != undefined) {
					selectedOption = value;
					getData();
					window.setInterval(getData, 10000);
					window.setInterval(displayTimer, 1000);
				}

			}).fail(function(jqxhr, textStatus, error) {
		var err = textStatus + ", " + error;
		alert("Request Failed: " + err);
	});

	$('#selectBox').on('change', function() {
		selectedOption = this.value;
		getData();

	});
}

function getData() {

	//$.getJSON(
	//		"http://localhost:8080/Voting/secure/" + selectedOption
	//				+ "/graph")
	 $.getJSON("http://voting.elasticbeanstalk.com/secure/"+ selectedOption
				+ "/graph")
	.done(function(json) {
		
		generateChart(json);

	}).fail(function(jqxhr, textStatus, error) {
		var err = textStatus + ", " + error;
		alert("Request Failed: " + err);
	});
}

function displayTimer() {
	$('#Refresh').text(--teller + "");
	if (teller == 0) {
		teller = 10;
	}
}

function generateChart(json) {
	
	//var data = [{ "Answer": "Yes", "Female": 22000, "Male": 23356 }, { "Answer": "No", "Female": 27000, "Male": 21351 }, { "Answer": "Undecided", "Female": 4823, "Male": 8314}];
	var title = 'Question: ' + json.graph.question;
	var data = json.graph.datasource;
	if (chart1 == undefined || chart1 == null) {
		chart1 = createChart(cfx.Gallery.Pie, "PieSpotlight", data,
				title, "Answers", "Count", "ChartDiv");
	} else {
		chart1.setDataSource(data);
		chart1.update();
	}
	if (chart2 == undefined || chart2 == null) {
		chart2 = createChart(cfx.Gallery.bar, "BarSpotlight", data,
				title, "Answers", "Count", "ChartDiv2");
	} else {
		chart2.setDataSource(data);
		chart2.update();
	}

}

function createChart(gallery, template, data, title, axisX, axisY, div) {
	var chart = new cfx.Chart();
	chart.setGallery(gallery);
	var pie = chart.getGalleryAttributes();
	pie.setTemplate(template);

	chart.getAxisX().getTitle().setText(axisX);
	chart.getAxisY().getTitle().setText(axisY);

	/*var newTitle;
	newTitle = new cfx.TitleDockable();
	newTitle.setText(title);
	newTitle.setFont("14pt arial");
	newTitle.setDock(cfx.DockArea.Top);
	chart.getTitles().add(newTitle);*/
	

	chart.getLegendBox().setVisible(true);
	chart.getAllSeries().getPointLabels().setVisible(true);
	chart.getAnimations().getLoad().setEnabled(true);

	chart.setDataSource(data);

	var divHolder = document.getElementById(div);
	chart.create(divHolder);
	return chart;
}

function get(name) {
	if (name = (new RegExp('[?&]' + encodeURIComponent(name) + '=([^&]*)'))
			.exec(location.search))
		return decodeURIComponent(name[1]);
}
