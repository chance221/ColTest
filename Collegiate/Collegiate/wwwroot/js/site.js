// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var date;
var offers = Array();
var listOfOffers = [];

//event handler to display data in modal content on modal open
$('#OffersModal').on('show.bs.modal', function (event) {
    date = $(event.relatedTarget).data('date');
    offers = Array.from($(event.relatedTarget).data('offers'));

    listOfOffers = [];
    offers.forEach(displayOffers);
    listOfOffers.forEach(offerDisplay);
});

//event handler to reset modal content on modal hide
$('#OffersModal').on('hide.bs.modal', function (event) {
    $('#offersTable').find(".offersTableBody").text("");
});

//function to get list of offers for a given day
function displayOffers(index, item) {
    if (index.DepartureTime.Day == date.Day) {
        listOfOffers.push(index);
    }
}

//function to display offers for a given day in modal content
function offerDisplay(index, item) {
    var departureHours = new Date(index.DepartureTime).getHours();
    departureHours = (departureHours % 12) || 12
    var departureMinutes = new Date(index.DepartureTime).getUTCMinutes();
    departureMinutes = addZeros(departureMinutes)

    var arrivalHours = new Date(index.ArrivalTime).getHours();
    arrivalHours = (arrivalHours % 12) || 12
    var arrivalMinutes = new Date(index.ArrivalTime).getUTCMinutes();
    arrivalMinutes = addZeros(arrivalMinutes)

    $('#offersTable').find('.offersTableBody').append(
        "<tr><td>" + index.Destination.City + "</td>" +
        "<td>" + index.Campus + "</td>" +
        "<td>" + departureHours + ":" + departureMinutes + "</td>" +
        "<td>" + arrivalHours + ":" + arrivalMinutes + "</td>" +
        "<td><button type='button' class='btn-success shadow-lg text-white p-1 rounded d-flex justify-content-center mx-auto'>Details</button></td>" +
        "</tr>");
}

//function to add extra zeros to time in dates
//use for better readability
function addZeros(mins) {
    return (mins < 10 ? '0' : '') + mins;
}

$('.prevMonth').on('toggle', function (event) {
    var calendar = document.querySelector("calendar");

    calendar.setAttribute("month", "1");
});