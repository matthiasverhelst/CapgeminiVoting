var barChart;
var chart2;
var teller = 10;
var dataTimer;
var displayTimer;
var selectedOption;
var chartMaxValue = 4;
google.load("visualization", "1", { packages: ["corechart"] });

$(document).ready(function ($) {
	getQuestions();
});

function getQuestions() {

	 $.getJSON(viewBag.questionsLink).done(
		function(questions) {
			var select = $("#selectBox");
				
			if (questions != null && questions.length > 0) {

				for (var i = 0; i < questions.length; i++) {
					var value = '<option value="' + questions[i].Id + '">'
							+ questions[i].Question + '</option>';

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
		}

    ).fail(function (jqxhr, textStatus, error) {
		var err = textStatus + ", " + error;
		alert("Request Failed: " + err);
	});

	$('#selectBox').on('change', function() {
		selectedOption = this.value;
		getData();
	});
}

function getData() {

	 $.getJSON(viewBag.questionResult + "?questionId=" + selectedOption)
	.done(function(json) {
	    $("#voterCount").text(json.VoterCount);
	    var dataArray = new Array(new Array({ label: "Answer", id: "answer" },
                                            { label: "Votes", id: "votes", type: "number" },
                                            { role: 'style' }));
	    $.each(json.AnswerResult, function (key, value) {
	        var intValue = parseInt(value);
	        dataArray.push(new Array(key, intValue, 'color: #76A7FA'));
	        if (intValue > chartMaxValue)
	            chartMaxValue = calculateChartMaxValue(value);
	    });
		generateChart(json.Question, dataArray);
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

function generateChart(question, dataArray) {
    var data = google.visualization.arrayToDataTable(dataArray);

    var view = new google.visualization.DataView(data);
    view.setColumns([0, 1, {
                             calc: "stringify",
                             sourceColumn: 1,
                             type: "string",
                             role: "annotation"
                            },
                     2]);

    var options = {
        title: question,
        width: 1000,
        height: 600,
        fontSize: 24,
        vAxis: { minValue: 0, maxValue: chartMaxValue }
    };

    var chart = new google.visualization.ColumnChart(document.getElementById('chart'));
    chart.draw(view, options);
}

function calculateChartMaxValue(value) {
    while (value % 4 != 0) {
        value++;
    }

    return value;
}