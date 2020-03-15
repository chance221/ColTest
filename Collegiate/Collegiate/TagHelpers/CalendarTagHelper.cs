using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Collegiate.Models;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace Collegiate.TagHelpers
{
    [HtmlTargetElement("calendar", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class CalendarTagHelper : TagHelper
    {
        #region PROPERTIES

        private int _month;
        private int _year;
        private ICollection<DriverOffer> _offers;
        private ICollection<RiderRequest> _requests;
        private List<DateTime> _dates;
        private List<DriverOffer> _dailyOffers;
        private DateTime _currentDate;

        #endregion

        #region FIELDS

        public ICollection<DriverOffer> Offers
        {
            get { return _offers; }
            set { _offers = value; }
        }
        public ICollection<RiderRequest> Requests
        {
            get { return _requests; }
            set { _requests = value; }
        }

        public int Year
        {
            get { return _year; }
            set { _year = value; }
        }

        public int Month
        {
            get { return _month; }
            set { _month = value; }
        }

        public List<DateTime> Dates
        {
            get { return _dates; }
            set { _dates = value; }
        }

        public DateTime CurrentDate
        {
            get { return _currentDate; }
            set { _currentDate = value; }
        }

        public List<DriverOffer> DailyOffers
        {
            get { return _dailyOffers; }
            set { _dailyOffers = value; }
        }

        #endregion

        #region METHODS

        //generates calendar tag and its respective html
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            Offers = GetDriverOffers();

            output.TagName = "section";
            output.Attributes.Add("class", "calendar");
            output.Content.SetHtmlContent(GetHtml());
            output.TagMode = TagMode.StartTagAndEndTag;            
        }

        //generates calendar html
        private string GetHtml()
        {                  
            //current month
            var monthStart = new DateTime(Year, Month, 1);

            //generate calendar body
            var html = new XDocument(
                new XElement("div",
                    new XAttribute("class", "container-fluid w-76"),
                    new XElement("header",

                        //title
                        new XElement("h4",
                            new XAttribute("class", "display-4 mb-2 text-center"),

                            //button to navigate to previous month
                            new XElement("button", new XAttribute("class", "prevMonth btn btn-lg m-2 mt-0 p-1"), HttpUtility.HtmlDecode("&#x276C;")),

                            //date
                            monthStart.ToString("MMMM, "),
                            monthStart.Year.ToString(),

                            //button to navigate to next month
                            new XElement("button", new XAttribute("class", "nextMonth btn btn-lg m-2 p-1"), HttpUtility.HtmlDecode("&#x276D;")) 
                        ),

                        //days of the week
                        new XElement("div",
                            new XAttribute("class", "row d-none d-lg-flex bg-success text-white"),
                            Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>().Select(d =>
                                new XElement("h6",
                                    new XAttribute("class", "col-lg p-1 text-center"),
                                    d.ToString()
                                )
                            )
                        )
                    ),

                    //generate dates
                    new XElement("div",
                    new XAttribute("class", "row border border-right-0 border-bottom-0"),
                        GetDatesHtml()
                    )
                )
            );

            return html.ToString();            
        }

        //gets and returns dates html
        IEnumerable<XElement> GetDatesHtml()
        {
            //current month
            var monthStart = new DateTime(Year, Month, 1);

            //start date
            var startDate = monthStart.AddDays(-(int)monthStart.DayOfWeek);

            //dates, Range(0, 42) to account for days of previous month and next month
            var dates = Enumerable.Range(0, 42).Select(i => startDate.AddDays(i));

            foreach (var d in dates)
            {
                //generate html element to 
                if (d.DayOfWeek == DayOfWeek.Sunday && d != startDate)
                {
                    yield return new XElement("div",
                        new XAttribute("class", "w-100"),
                        string.Empty
                    );
                }

                //classes used to emphasize previous/next month's days
                var mutedClasses = "d-none d-lg-inline-block bg-light text-muted";

                //generate individual date html
                yield return new XElement("div",
                new XAttribute("class", $"day col-lg p-2 border border-left-0 border-top-0 text-wrap {(d.Month != monthStart.Month ? mutedClasses : null)}"),
                new XElement("h6",
                    new XAttribute("class", "row align-items-right"),
                    new XElement("span",
                        new XAttribute("class", "date col-1 ml-2"),
                        d.Day
                    ),
                    new XElement("small",
                        new XAttribute("class", "col d-lg-none text-center text-muted"),
                        d.DayOfWeek.ToString()
                    ),
                    new XElement("span",
                        new XAttribute("class", "col-1"),
                        String.Empty
                    )
                ),

                //get offers html
                GetOfferHtml(d),

                //get requests html
                GetRequestHtml(d)
                );
            }
        }

        //gets and returns requests html
        IEnumerable<XElement> GetRequestHtml(DateTime d)
        {
            #region code

            //if (loggedDates.Count != 0)
            //{
            //    loggedDates.Add(d);
            //    foreach (var date in loggedDates)
            //    {
            //        if (d == date)
            //        {
            //            return null;
            //        }
            //        else
            //        {
            //            foreach (var offer in offers)
            //            {
            //                if (offer.Key == d)
            //                {
            //                    i++;
            //                }
            //                else
            //                {
            //                    continue;
            //                }
            //            }

            //            return offers?.SingleOrDefault(e => e.Key == d)?.Select(e =>
            //            new XElement("a",
            //                new XAttribute("campus", e.Campus),
            //                new XAttribute("arrivalTime", e.ArrivalTime),
            //                new XAttribute("class", "btn-success text-white p-1 "),
            //                "Offers: " + i
            //            )

            //            );
            //            //    ?? new[] {
            //            //new XElement("p",
            //            //    new XAttribute("class", "d-lg-none"),
            //            //    ""
            //            //)
            //            //};
            //        }
            //    }
            //}

            //loggedDates.Add(d);

            //foreach (var offer in offers)
            //{
            //    if (offer.Key == d)
            //    {
            //        i++;
            //    }
            //    else
            //    {
            //        continue;
            //    }
            //}

            #endregion
            #region keep for now
            //    ?? new[] {
            //new XElement("p",
            //    new XAttribute("class", "d-lg-none"),
            //    ""
            //)
            //};
            #endregion

            //get all requests
            Requests = GetRiderRequests();
            var requests = Requests?.GroupBy(r => r.DepartureTime);

            //foreach (var request in requests)
            //{
            //    if (request.Key == d)
            //    {
            //        Dates.Add(d);
            //    }
            //}

            return requests?.FirstOrDefault(r => r.Key == d)?.Select(r =>
            new XElement("a",
            new XAttribute("campus", r.Campus),
            new XAttribute("arrivalTime", r.ArrivalTime),
            new XAttribute("class", "btn-warning text-white p-1 rounded d-flex justify-content-center w-75 mx-auto mt-1 mb-30"),
            new XAttribute("data-toggle", "modal"),
            new XAttribute("data-target", "#RequestsModal"),
            new XAttribute("data-date", $"{d.Date}"),
            "Requests"
                )

            );

        }

        //gets and returns offers html
        IEnumerable<XElement> GetOfferHtml(DateTime d)
        {            

            CurrentDate = d;
            Offers = GetDriverOffers();
            DailyOffers = GetDailyOffers(Offers.ToList());
            Offers = GetDriverOffers().Distinct().ToList();
            var offers = Offers?.GroupBy(o => o.DepartureTime);

            string test = JsonConvert.SerializeObject(DailyOffers);
                                    
            return offers?.FirstOrDefault(o => o.Key == d)?.Select(o =>
                new XElement("a",
                new XAttribute("campus", o.Campus),
                new XAttribute("arrivalTime", o.ArrivalTime),
                new XAttribute("class", "btn-success text-white p-1 rounded d-flex justify-content-center w-75 mx-auto"),
                new XAttribute("data-toggle", "modal"),
                new XAttribute("data-target", "#OffersModal"),
                new XAttribute("data-date", $"{d.Date}"),
                new XAttribute("data-offers", $"{test}"),
                "Offers"
                )
            );          
        }

        //seed data for DriverOffers, returns a list
        public List<DriverOffer> GetDriverOffers()
        {
            List<DriverOffer> offers = new List<DriverOffer>
            {
                new DriverOffer
                {
                    DepartureTime = new DateTime(2020, 3, 26),
                    Campus = "Main Campus",
                    ArrivalTime = new DateTime(2020, 3, 17, 13, 0, 0),
                    Destination = new Address
                    {
                        Address1 = "123 Example St.",
                        City = "Traverse City"
                    }
                },

                new DriverOffer
                {
                    DepartureTime = new DateTime(2020, 3, 26),
                    Campus = "Parson Stuelen",
                    ArrivalTime = new DateTime(2020, 3, 17, 13, 0, 0),
                    Destination = new Address
                    {
                        Address1 = "123 Example St.",
                        City = "Traverse City"
                    }
                },

                new DriverOffer
                {
                    DepartureTime = new DateTime(2020, 3, 1),
                    Campus = "Aero Park Campus",
                    ArrivalTime = new DateTime(2020, 3, 20, 10, 15, 0),
                    Destination = new Address
                    {
                        Address1 = "123 Example St.",
                        City = "Elk Rapids"
                    }
                 },

                new DriverOffer
                {
                    DepartureTime = new DateTime(2020, 3, 3),
                    Campus = "Universy Center",
                    ArrivalTime = new DateTime(2020, 3, 3, 15, 30, 0),
                    Destination = new Address
                    {
                        Address1 = "123 Example St.",
                        City = "Suttons Bay"
                    }
                },

                new DriverOffer
                {
                    DepartureTime = new DateTime(2020, 3, 25),
                    Campus = "Universy Center",
                    ArrivalTime = new DateTime(2020, 3, 25, 15, 30, 0),
                    Destination = new Address
                    {
                        Address1 = "123 Example St.",
                        City = "Elk Rapids"
                    }
                },

                new DriverOffer
                {
                    DepartureTime = new DateTime(2020, 3, 10),
                    Campus = "Universy Center",
                    ArrivalTime = new DateTime(2020, 3, 10, 15, 30, 0),
                    Destination = new Address
                    {
                        Address1 = "123 Example St.",
                        City = "Northport"
                    }
                },

                new DriverOffer
                {
                    DepartureTime = new DateTime(2020, 3, 13),
                    Campus = "Universy Center",
                    ArrivalTime = new DateTime(2020, 3, 13, 15, 30, 0),
                    Destination = new Address
                    {
                        Address1 = "123 Example St.",
                        City = "Empire"
                    }
                },

                new DriverOffer
                {
                    DepartureTime = new DateTime(2020, 3, 23),
                    Campus = "Universy Center",
                    ArrivalTime = new DateTime(2020, 3, 23, 15, 30, 0),
                    Destination = new Address
                    {
                        Address1 = "123 Example St.",
                        City = "Empire"
                    }
                },
                new DriverOffer
                {
                    DepartureTime = new DateTime(2020, 3, 26),
                    Campus = "University Center",
                    ArrivalTime = new DateTime(2020, 3, 17, 13, 0, 0),
                    Destination = new Address
                    {
                        Address1 = "123 Example St.",
                        City = "Northport"
                    }
                },

                new DriverOffer
                {
                    DepartureTime = new DateTime(2020, 3, 26),
                    Campus = "Great Lakes Campus",
                    ArrivalTime = new DateTime(2020, 3, 17, 10, 5, 5),
                    Destination = new Address
                    {
                        Address1 = "123 Example St.",
                        City = "Suttons Bay"
                    }
                },

                new DriverOffer
                {
                    DepartureTime = new DateTime(2020, 3, 23),
                    Campus = "Universy Center",
                    ArrivalTime = new DateTime(2020, 3, 23, 15, 30, 0),
                    Destination = new Address
                    {
                        Address1 = "123 Example St.",
                        City = "Traverse City"
                    }
                },
            };

            return offers;
        }

        //seed data for RiderRequests, returns a list
        public List<RiderRequest> GetRiderRequests()
        {
            List<RiderRequest> requests = new List<RiderRequest>
            {
                new RiderRequest
                {
                    DepartureTime = new DateTime(2020, 3, 17),
                    Campus = "Main Campus",
                    ArrivalTime = new DateTime(2020, 3, 17, 13, 0, 0)
                },

                new RiderRequest
                {
                    DepartureTime = new DateTime(2020, 3, 20),
                    Campus = "Aero Park Campus",
                    ArrivalTime = new DateTime(2020, 3, 20, 10, 15, 0)
                },

                new RiderRequest
                {
                    DepartureTime = new DateTime(2020, 3, 19),
                    Campus = "Universy Center",
                    ArrivalTime = new DateTime(2020, 3, 19, 15, 30, 0)
                },

                new RiderRequest
                {
                    DepartureTime = new DateTime(2020, 3, 25),
                    Campus = "Universy Center",
                    ArrivalTime = new DateTime(2020, 3, 25, 15, 30, 0)
                },

                new RiderRequest
                {
                    DepartureTime = new DateTime(2020, 3, 5),
                    Campus = "Universy Center",
                    ArrivalTime = new DateTime(2020, 3, 5, 15, 30, 0)
                },

                new RiderRequest
                {
                    DepartureTime = new DateTime(2020, 3, 13),
                    Campus = "Universy Center",
                    ArrivalTime = new DateTime(2020, 3, 13, 15, 30, 0)
                },

                new RiderRequest
                {
                    DepartureTime = new DateTime(2020, 3, 31),
                    Campus = "Universy Center",
                    ArrivalTime = new DateTime(2020, 3, 28, 15, 30, 0)
                },

                new RiderRequest
                {
                    DepartureTime = new DateTime(2020, 3, 10),
                    Campus = "Universy Center",
                    ArrivalTime = new DateTime(2020, 3, 10, 15, 30, 0)
                },
            };

            return requests;
        }

        //gets and returns a list of offers for a given day, to be displayed when a user clicks a button on the calendar
        public List<DriverOffer> GetDailyOffers(List<DriverOffer> offers)
        {
            List<DriverOffer> offersOfTheDay = new List<DriverOffer>();

            foreach (DriverOffer offer in offers)
            {
                if (
                    offer.DepartureTime.Year == CurrentDate.Year 
                    && offer.DepartureTime.Month == CurrentDate.Month 
                    && offer.DepartureTime.Day == CurrentDate.Day)                    
                {
                    offersOfTheDay.Add(offer);
                }
            }

            return offersOfTheDay;
        }

        #endregion
    }
}