window.scheduleData = [{
    Id: 1,
    Subject: "Weekly Recurrence.",
    StartTime: new Date(new Date().getTime() + 86400 * 1000 * 1).setHours(9, 30),
    EndTime: new Date(new Date().getTime() + 86400 * 1000 * 1).setHours(10, 30),
    AllDay: false,
    Recurrence: true,
    RecurrenceRule: "FREQ=WEEKLY;COUNT=10;INTERVAL=1;BYDAY=MO,TU"
},
    {
        Id: 2,
        Subject: "Project Review.",
        StartTime: new Date().setHours(7, 00),
        EndTime: new Date().setHours(8, 00),
        AllDay: false,
        Recurrence:false
    },
    {
        Id: 3,
        Subject: "Daily Recurrence.",
        StartTime: new Date(new Date().getTime() + 86400 * 1000 * 2).setHours(4, 30),
        EndTime: new Date(new Date().getTime() + 86400 * 1000 * 2).setHours(5, 30),
        AllDay: false,
        Recurrence: true,
        RecurrenceRule: "FREQ=DAILY;INTERVAL=1;COUNT=10"
    },
    {
        Id: 4,
        Subject: "Monthly Recurrence.",
        StartTime: new Date(new Date().getTime() + 86400 * 1000 * (-3)).setHours(11, 00),
        EndTime: new Date(new Date().getTime() + 86400 * 1000 * (-3)).setHours(11, 30),
        AllDay: false,
        Recurrence: true,
        RecurrenceRule: "FREQ=MONTHLY;COUNT=10;BYDAY=SU;BYSETPOS=1"
    },
    {
        Id: 5,
        Subject: "AllDay App.",
        StartTime: new Date(new Date().getTime() + 86400 * 1000 * (-1)).setHours(2, 30),
        EndTime: new Date(new Date().getTime() + 86400 * 1000 * (-1)).setHours(3, 00),
        AllDay: true,
        Recurrence: false
    },
    {
        Id: 6,
        Subject: "Yearly Recurrence.",
        StartTime: new Date(new Date().getTime() + 86400 * 1000 * (-2)).setHours(6, 30),
        EndTime: new Date(new Date().getTime() + 86400 * 1000 * (-2)).setHours(7, 00),
        AllDay: false,
        Recurrence: true,
        RecurrenceRule: "FREQ=YEARLY;COUNT=10;BYMONTHDAY=2;BYMONTH=3"
    }];

window.Default = [{
    Id: 100,
    Subject: "Bering Sea Gold",
    StartTime: new Date(new Date().setMinutes(new Date().getMinutes() + 6)),
    EndTime: new Date(new Date().setHours(new Date().getHours() + 2)),
    AllDay: false,
    Recurrence: true,
    RecurrenceRule: "FREQ=DAILY;INTERVAL=2;COUNT=10",
    Categorize:"3"
},
    {
        Id: 101,
        Subject: "Bering Sea Gold",
        StartTime:new Date().setHours(4, 0),
        EndTime: new Date().setHours(5, 0),
        AllDay: false,
        Recurrence: false,
        Categorize: "1,3"
    },
    {
        Id: 102,
        Subject: "Bering Sea Gold",
        StartTime: new Date().setHours(16, 0),
        EndTime: new Date().setHours(17, 30),
        AllDay: false,
        Recurrence: false,
        Categorize: "2,5"
    },
    {
        Id: 103,
        Subject: "What Happened Next?",
        StartTime: new Date(new Date().getTime() + 86400 * 1000 * 2).setHours(1, 0),
        EndTime: new Date(new Date().getTime() + 86400 * 1000 * 2).setHours(1, 30),
        AllDay: false,
        Recurrence: false,
        Categorize: "3,6"
    },
    {
        Id: 104,
        Subject: "What Happened Next?",
        StartTime: new Date(new Date().getTime() + 86400 * 1000 * 2).setHours(3, 30),
        EndTime: new Date(new Date().getTime() + 86400 * 1000 * 2).setHours(4, 0),
        AllDay: false,
        Recurrence: false,
        Categorize: "4,1"
    }, {
        Id: 105,
        Subject: "Daily Planet",
        StartTime: new Date(new Date().setMinutes(new Date().getMinutes() + 5)),
        EndTime: new Date(new Date().setHours(new Date().getHours() + 1)),
        AllDay: false,
        Recurrence: false,
        Categorize: "1,3,6"
    }, {
        Id: 106,
        Subject: "Alaska: The Last Frontier",
        StartTime: new Date(new Date().getTime() + 86400 * 1000 * 1).setHours(4,0),
        EndTime: new Date(new Date().getTime() + 86400 * 1000 * 1).setHours(5, 0),
        AllDay: false,
        Recurrence: false,
        Categorize: "2,3,4,5"
    }, {
        Id: 107,
        Subject: "How It's Made",
        StartTime: new Date(new Date().getTime() + 86400 * 1000 * -1).setHours(6, 0),
        EndTime: new Date(new Date().getTime() + 86400 * 1000 * -1).setHours(7, 30),
        AllDay: false,
        Recurrence: true,
        RecurrenceRule: "FREQ=WEEKLY;BYDAY=MO,TU;INTERVAL=1;COUNT=15",
        Categorize: "2,3,6"
    }, {
        Id: 108,
        Subject: "Deadest Catch",
        StartTime: new Date(new Date().getTime() + 86400 * 1000 * 1).setHours(16, 0),
        EndTime: new Date(new Date().getTime() + 86400 * 1000 * 1).setHours(17, 0),
        AllDay: false,
        Recurrence: false,
        Categorize: "2,4,6,1"
    }, {
        Id: 109,
        Subject: "MayDay",
        StartTime: new Date(new Date().getTime() + 86400 * 1000 * -2).setHours(6, 30),
        EndTime: new Date(new Date().getTime() + 86400 * 1000 * -2).setHours(7, 30),
        AllDay: false,
        Recurrence: false,
        Categorize: "5,3"
    }, {
        Id: 110,
        Subject: "MoonShiners",
        StartTime: new Date().setHours(2, 0),
        EndTime: new Date().setHours(2, 30),
        AllDay: false,
        Recurrence: true,
        RecurrenceRule: "FREQ=DAILY;INTERVAL=1;COUNT=5",
        Categorize: "6,2,5"
    }, {
        Id: 111,
        Subject: "Close Encounters",
        StartTime: new Date(new Date().getTime() + 86400 * 1000 * -2).setHours(14, 0),
        EndTime: new Date(new Date().getTime() + 86400 * 1000 * -2).setHours(15, 0),
        AllDay: false,
        Recurrence: true,
        RecurrenceRule: "FREQ=WEEKLY;BYDAY=MO,TH;INTERVAL=1;COUNT=5",
        Categorize: "3,4,5"
    }, {
        Id: 112,
        Subject: "Close Encounters",
        StartTime: new Date(new Date().getTime() + 86400 * 1000 * -2).setHours(3, 0),
        EndTime: new Date(new Date().getTime() + 86400 * 1000 * -2).setHours(3, 30),
        AllDay: false,
        Recurrence: true,
        RecurrenceRule: "FREQ=WEEKLY;BYDAY=WE;INTERVAL=1;COUNT=3",
        Categorize: "5,2,1"
    }, {
        Id: 113,
        Subject: "HighWay Thru Hell",
        StartTime: new Date(new Date().getTime() + 86400 * 1000 * -1).setHours(3, 0),
        EndTime: new Date(new Date().getTime() + 86400 * 1000 * -1).setHours(7, 0),
        AllDay: false,
        Recurrence: false,
        Categorize: "5,6,3"
    }, {
        Id: 114,
        Subject: "Moon Shiners",
        StartTime: new Date().setHours(4, 20),
        EndTime: new Date().setHours(5, 50),
        AllDay: false,
        Recurrence: false,
        Categorize: "1,2,3,4,5,6"
    }, {
        Id: 115,
        Subject: "Cash Cab",
        StartTime: new Date(new Date().getTime() + 86400 * 1000 * -2).setHours(15, 0),
        EndTime: new Date(new Date().getTime() + 86400 * 1000 * -2).setHours(16, 30),
        AllDay: false,
        Recurrence: true,
        RecurrenceRule: "FREQ=DAILY;INTERVAL=1;COUNT=5",
        Categorize: "1,3"
    }
    ];
    window.ResourcesData = [{
        Id: 100,
        Subject: "Bering Sea Gold",
        StartTime: new Date().setHours(9, 0),
        EndTime: new Date().setHours(10, 30),
        AllDay: false,
        Recurrence: true,
        RecurrenceRule: "FREQ=DAILY;INTERVAL=2;COUNT=10",
        categoryId: 1, roomId: 2, ownerId: 3
    }, {
        Id: 101,
        Subject: "Hello Sea Gold",
        StartTime: new Date().setHours(4, 0),
        EndTime: new Date().setHours(5, 0),
        AllDay: false,
        Recurrence: false,
        categoryId: 1, roomId: 2, ownerId: 3
    }, {
        Id: 105,
        Subject: "Daily Planet",
        StartTime: new Date(new Date().getTime() + 86400 * 1000 * 1).setHours(1, 0),
        EndTime: new Date(new Date().getTime() + 86400 * 1000 * 1).setHours(2, 0),
        AllDay: false,
        Recurrence: true,
        RecurrenceRule: "FREQ=DAILY;INTERVAL=2;COUNT=10",
        categoryId: 1, roomId: 1, ownerId: 1
    }, {
        Id: 106,
        Subject: "Alaska: The Last Frontier",
        StartTime: new Date(new Date().getTime() + 86400 * 1000 * 1).setHours(4, 0),
        EndTime: new Date(new Date().getTime() + 86400 * 1000 * 1).setHours(5, 0),
        AllDay: false,
        Recurrence: true,
        RecurrenceRule: "FREQ=DAILY;INTERVAL=2;COUNT=10",
        categoryId: 1, roomId: 1, ownerId: 5
    }, {
        Id: 109,
        Subject: "MayDay",
        StartTime: new Date(new Date().getTime() + 86400 * 1000 * -2).setHours(6, 30),
        EndTime: new Date(new Date().getTime() + 86400 * 1000 * -2).setHours(7, 30),
        AllDay: false,
        Recurrence: false,
        categoryId: 1, roomId: 2, ownerId: 3
    }];
    window.HorizontalResourcesData = [{
        Id: 100,
        Subject: "Bering Sea Gold",
        StartTime: new Date().setHours(9, 0),
        EndTime: new Date().setHours(10, 30),
        AllDay: false,
        Recurrence: true,
        RecurrenceRule: "FREQ=DAILY;INTERVAL=2;COUNT=10",
        categoryId: 1, roomId: 2, ownerId: 3
    }, {
        Id: 101,
        Subject: "Hello Sea Gold",
        StartTime: new Date().setHours(4, 0),
        EndTime: new Date().setHours(5, 0),
        AllDay: false,
        Recurrence: false,
        categoryId: 1, roomId: 2, ownerId: 3
    }, {
        Id: 105,
        Subject: "Daily Planet",
        StartTime: new Date(new Date().getTime() + 86400 * 1000 * 1).setHours(1, 0),
        EndTime: new Date(new Date().getTime() + 86400 * 1000 * 1).setHours(2, 0),
        AllDay: false,
        Recurrence: true,
        RecurrenceRule: "FREQ=DAILY;INTERVAL=2;COUNT=5",
        categoryId: 1, roomId: 1, ownerId: 1
    }, {
        Id: 106,
        Subject: "Alaska: The Last Frontier",
        StartTime: new Date(new Date().getTime() + 86400 * 1000 * 1).setHours(4, 0),
        EndTime: new Date(new Date().getTime() + 86400 * 1000 * 2).setHours(5, 0),
        AllDay: false,
        Recurrence: true,
        RecurrenceRule: "FREQ=DAILY;INTERVAL=2;COUNT=7",
        categoryId: 1, roomId: 1, ownerId: 5
    }, {
        Id: 109,
        Subject: "MayDay",
        StartTime: new Date(new Date().getTime() + 86400 * 1000 * -2).setHours(6, 30),
        EndTime: new Date(new Date().getTime() + 86400 * 1000 * -2).setHours(7, 30),
        AllDay: false,
        Recurrence: false,
        categoryId: 1, roomId: 2, ownerId: 3
    }
    ];
window.Template = [{
    Id: 200,
    Subject: "Basketball Practice",
    StartTime: new Date().setHours(4, 0),
    EndTime: new Date().setHours(5, 0),
    AllDay: false,
    Recurrence: true,
    RecurrenceRule: "FREQ=WEEKLY;BYDAY=MO;INTERVAL=1;COUNT=5"
}, {
    Id: 201,
    Subject: "Rugby Match",
    StartTime: new Date(new Date().getTime() + 86400 * 1000 * 1).setHours(8, 45),
    EndTime: new Date(new Date().getTime() + 86400 * 1000 * 1).setHours(9, 45),
    AllDay: false,
    Recurrence: true,
    RecurrenceRule: "FREQ=WEEKLY;BYDAY=TU;INTERVAL=1;COUNT=5"
},
    {
        Id: 202,
        Subject: "Guitar Class",
        StartTime: new Date(new Date().getTime() + 86400 * 1000 * 2).setHours(5, 30),
        EndTime: new Date(new Date().getTime() + 86400 * 1000 * 2).setHours(6, 30),
        AllDay: false,
        Recurrence: true,
        RecurrenceRule: "FREQ=WEEKLY;BYDAY=WE;INTERVAL=1;COUNT=5"
    },
    {
        Id: 203,
        Subject: "Music Lessons",
        StartTime: new Date(new Date().getTime() + 86400 * 1000 * 3).setHours(4, 0),
        EndTime: new Date(new Date().getTime() + 86400 * 1000 * 3).setHours(5, 30),
        Description: "Highland Academy",
        AllDay: false,
        Recurrence: true,
        RecurrenceRule: "FREQ=WEEKLY;BYDAY=TH;INTERVAL=1;COUNT=5"
    },
{
    Id: 204,
    Subject: "Grandpa Birthday",
    StartTime: new Date(new Date().getTime() + 86400 * 1000 * 4).setHours(0, 0),
    EndTime: new Date(new Date().getTime() + 86400 * 1000 * 4).setHours(1, 0),
    AllDay: false,
    Recurrence: true,
    RecurrenceRule: "FREQ=WEEKLY;BYDAY=SU;INTERVAL=1;COUNT=1"
},
{
    Id: 205,
    Subject: "No School (In-service)",
    StartTime: new Date(new Date().getTime() + 86400 * 1000 * 5).setHours(5, 0),
    EndTime: new Date(new Date().getTime() + 86400 * 1000 * 5).setHours(6, 30),
    AllDay: false,
    Recurrence: true,
    RecurrenceRule: "FREQ=WEEKLY;BYDAY=SA;INTERVAL=1;COUNT=3"
},
{
    Id: 209,
    Subject: "Doctor checkup",
    StartTime: new Date(new Date().getTime() + 86400 * 1000 * 6).setHours(10, 15),
    EndTime: new Date(new Date().getTime() + 86400 * 1000 * 6).setHours(11, 30),
    AllDay: false,
    Recurrence: true,
    RecurrenceRule: "FREQ=WEEKLY;BYDAY=FR;INTERVAL=2;COUNT=5"
}];

window.Startend = [
{
    Id: 400,
    Subject: "Brazil - Croatia",
    StartTime: new Date().setHours(17, 0),
    EndTime: new Date().setHours(18, 30),
    AllDay: false,
    Recurrence: false
},
{
    Id: 401,
    Subject: "Mexico - Cameroon",
    StartTime: new Date(new Date().getTime() + 86400 * 1000 * 1).setHours(13, 0),
    EndTime: new Date(new Date().getTime() + 86400 * 1000 * 1).setHours(14, 30),
    AllDay: false,
    Recurrence: false
},
{
    Id: 402,
    Subject: "Brazil - Mexico",
    StartTime: new Date(new Date().getTime() + 86400 * 1000 * 5).setHours(16, 0),
    EndTime: new Date(new Date().getTime() + 86400 * 1000 * 5).setHours(17, 30),
    AllDay: false,
    Recurrence: false
},
{
    Id: 403,
    Subject: "Cameroon - Croatia",
    StartTime: new Date(new Date().getTime() + 86400 * 1000 * 6).setHours(18, 0),
    EndTime: new Date(new Date().getTime() + 86400 * 1000 * 6).setHours(19, 30),
    AllDay: false,
    Recurrence: false
},
{
    Id: 404,
    Subject: "Cameroon - Brazil",
    StartTime: new Date(new Date().getTime() + 86400 * 1000 * 11).setHours(17, 0),
    EndTime: new Date(new Date().getTime() + 86400 * 1000 * 11).setHours(18, 30),
    AllDay: false,
    Recurrence: false
},
{
    Id: 405,
    Subject: "Croatia - Mexico",
    StartTime: new Date(new Date().getTime() + 86400 * 1000 * 10).setHours(17, 0),
    EndTime: new Date(new Date().getTime() + 86400 * 1000 * 10).setHours(18, 30),
    AllDay: false,
    Recurrence: false
},
{
    Id: 406,
    Subject: "Spain - Netherlands",
    StartTime: new Date(new Date().getTime() + 86400 * 1000 * 1).setHours(16, 0),
    EndTime: new Date(new Date().getTime() + 86400 * 1000 * 1).setHours(17, 30),
    AllDay: false,
    Recurrence: false
},
{
    Id: 407,
    Subject: "Chile - Australia",
    StartTime: new Date(new Date().getTime() + 86400 * 1000 * 1).setHours(18, 0),
    EndTime: new Date(new Date().getTime() + 86400 * 1000 * 1).setHours(19, 30),
    AllDay: false,
    Recurrence: false
},
{
    Id: 408,
    Subject: "Spain - Chile",
    StartTime: new Date(new Date().getTime() + 86400 * 1000 * 6).setHours(16, 0),
    EndTime: new Date(new Date().getTime() + 86400 * 1000 * 6).setHours(17, 30),
    AllDay: false,
    Recurrence: false
},
{
    Id: 409,
    Subject: "Australia - Netherlands",
    StartTime: new Date(new Date().getTime() + 86400 * 1000 * 6).setHours(13, 0),
    EndTime: new Date(new Date().getTime() + 86400 * 1000 * 6).setHours(14, 30),
    AllDay: false,
    Recurrence: false
},
{
    Id: 410,
    Subject: "Australia - Chile",
    StartTime: new Date(new Date().getTime() + 86400 * 1000 * 11).setHours(13, 0),
    EndTime: new Date(new Date().getTime() + 86400 * 1000 * 11).setHours(14, 30),
    AllDay: false,
    Recurrence: false
},
{
    Id: 411,
    Subject: "Netherlands - Chile",
    StartTime: new Date(new Date().getTime() + 86400 * 1000 * 10).setHours(13, 0),
    EndTime: new Date(new Date().getTime() + 86400 * 1000 * 10).setHours(14, 30),
    AllDay: false,
    Recurrence: false
}];

window.Timemode = [{
    Id: 500,
    Subject: "New training",
    StartTime: new Date().setHours(10, 0),
    EndTime: new Date().setHours(11, 30),
    AllDay: false,
    Recurrence: false
}, {
    Id: 501,
    Subject: "Conference meeting",
    StartTime: new Date().setHours(14, 0),
    EndTime: new Date().setHours(16, 0),
    AllDay: false,
    Recurrence: true,
    RecurrenceRule: "FREQ=WEEKLY;BYDAY=MO,TH;INTERVAL=2;COUNT=20"
}, {
    Id: 502,
    Subject: "Product meeting",
    StartTime: new Date(new Date().getTime() + 86400 * 1000 * 2).setHours(11, 0),
    EndTime: new Date(new Date().getTime() + 86400 * 1000 * 2).setHours(13, 0),
    AllDay: false,
    Recurrence: false
}, {
    Id: 503,
    Subject: "Evaluation review",
    StartTime: new Date(new Date().getTime() + 86400 * 1000 * -1).setHours(16, 0),
    EndTime: new Date(new Date().getTime() + 86400 * 1000 * -1).setHours(17, 0),
    AllDay: false,
    Recurrence: false
}, {
    Id: 504,
    Subject: "Case study",
    StartTime: new Date().setHours(6, 0),
    EndTime: new Date(new Date().getTime() + 86400 * 1000 * 1).setHours(18, 0),
    AllDay: true,
    Recurrence: false
}];

window.Viewcustomization = [{ Id: 600,
    Subject: "Ladies Mogals Qualification",
    StartTime: new Date().setHours(18, 0),
    EndTime: new Date().setHours(20, 0),
    AllDay: false,
    Recurrence: true,
    RecurrenceRule: "FREQ=WEEKLY;BYDAY=TU,TH;INTERVAL=2;COUNT=6"
}, {
    Id: 601,
    Subject: "Men's Mogals Qualification",
    StartTime: new Date().setHours(14, 0),
    EndTime: new Date().setHours(16, 0),
    AllDay: false,
    Recurrence: true,
    RecurrenceRule: "FREQ=WEEKLY;BYDAY=TU,TH;INTERVAL=1;COUNT=12"
}, {
    Id: 602,
    Subject: "Men's 500m race",
    StartTime: new Date(new Date().getTime() + 86400 * 1000 * 1).setHours(17, 0),
    EndTime: new Date(new Date().getTime() + 86400 * 1000 * 1).setHours(18, 0),
    AllDay: false,
    Recurrence: true,
    RecurrenceRule: "FREQ=DAILY;INTERVAL=1;COUNT=5"
}, {
    Id: 603,
    Subject: "Opening ceremony",
    StartTime: new Date(new Date().getTime() + 86400 * 1000 * -1).setHours(9, 0),
    EndTime: new Date(new Date().getTime() + 86400 * 1000 * -1).setHours(12, 0),
    AllDay: false,
    Recurrence: false
}, {
    Id: 604,
    Subject: "Finals",
    StartTime: new Date(new Date().getTime() + 86400 * 1000 * 2).setHours(10, 0),
    EndTime: new Date(new Date().getTime() + 86400 * 1000 * 2).setHours(17, 30),
    AllDay: false,
    Recurrence: false
}, {
    Id: 605,
    Subject: "Final presentation",
    StartTime: new Date(new Date().getTime() + 86400 * 1000 * 2).setHours(18, 0),
    EndTime: new Date(new Date().getTime() + 86400 * 1000 * 2).setHours(19, 30),
    AllDay: false,
    Recurrence: false
}];

window.Events = [{
    Id: 700,
    Subject: "Packing Day",
    StartTime: new Date().setHours(5, 0),
    EndTime: new Date().setHours(5, 30),
    AllDay: false,
    Recurrence: false
},
{
    Id: 701,
    Subject: "Spring Break",
    StartTime: new Date(new Date().getTime() + 86400 * 1000 * 2).setHours(6, 0),
    EndTime: new Date(new Date().getTime() + 86400 * 1000 * 2).setHours(7, 30),
    AllDay: false,
    Recurrence: false
},
{
    Id: 702,
    Subject: "Registration for Spring Term",
    StartTime: new Date().setHours(7, 0),
    EndTime: new Date().setHours(7, 30),
    AllDay: false,
    Recurrence: false
},
{
    Id: 703,
    Subject: "Last meal of winter term in LDC",
    StartTime: new Date().setHours(3, 00),
    EndTime: new Date().setHours(4, 50),
    AllDay: false,
    Recurrence: false
},
{
    Id: 704,
    Subject: "Sayles Cafe - Reduced Hours",
    StartTime: new Date(new Date().getTime() + 86400 * 1000 * -2).setHours(8, 0),
    EndTime: new Date(new Date().getTime() + 86400 * 1000 * -2).setHours(9, 30),
    AllDay: false,
    Recurrence: false
},
{
    Id: 705,
    Subject: "Bookstore Break Hours",
    StartTime: new Date().setHours(8, 30),
    EndTime: new Date().setHours(9, 0),
    AllDay: false,
    Recurrence: false
},
{
    Id: 706,
    Subject: "CFR 'Office Hours'",
    StartTime: new Date().setHours(11, 30),
    EndTime: new Date().setHours(13, 0),
    AllDay: false,
    Recurrence: false
},
{
    Id: 707,
    Subject: "Used Textbook Buyback/Text Rental Return",
    StartTime: new Date(new Date().getTime() + 86400 * 1000 * 1).setHours(10, 0),
    EndTime: new Date(new Date().getTime() + 86400 * 1000 * 1).setHours(13, 0),
    AllDay: false,
    Recurrence: false
},
{
    Id: 708,
    Subject: "Halls and Houses Close for winter Term at 2 p.m.",
    StartTime: new Date().setHours(14, 0),
    EndTime: new Date().setHours(15, 0),
    AllDay: false,
    Recurrence: false
},
{
    Id: 709,
    Subject: "Dining Dollars Expire Today at 2:00 pm",
    StartTime: new Date(new Date().getTime() + 86400 * 1000 * -1).setHours(14, 0),
    EndTime: new Date(new Date().getTime() + 86400 * 1000 * -1).setHours(16, 0),
    AllDay: false,
    Recurrence: false
}];

window.API = [{
    Id: 800,
    Subject: "Elementary italian I",
    StartTime: new Date().setHours(10, 10),
    EndTime: new Date().setHours(11, 0),
    AllDay: false,
    Recurrence: true,
    RecurrenceRule: "FREQ=WEEKLY;BYDAY=MO,TU,WE,TH;INTERVAL=1;COUNT=12"
}, {
    Id: 801,
    Subject: "Earth: Our Habitable Planet(lecture)",
    StartTime: new Date().setHours(13, 25),
    EndTime: new Date().setHours(14, 15),
    AllDay: false,
    Recurrence: true,
    RecurrenceRule: "FREQ=WEEKLY;BYDAY=TU,TH;INTERVAL=1;COUNT=6"
}, {
    Id: 802,
    Subject: "Earth: Our Habitable Planet(lecture)",
    StartTime: new Date().setHours(12, 20),
    EndTime: new Date().setHours(14, 15),
    AllDay: false,
    Recurrence: false
}, {
    Id: 803,
    Subject: "Yoga",
    StartTime: new Date().setHours(12, 20),
    EndTime: new Date().setHours(14, 15),
    AllDay: false,
    Recurrence: true,
    RecurrenceRule: "FREQ=WEEKLY;BYDAY=FR;INTERVAL=1;COUNT=3"
}, {
    Id: 804,
    Subject: "Intro to Computers(lab)",
    StartTime: new Date().setHours(14, 30),
    EndTime: new Date().setHours(15, 45),
    AllDay: false,
    Recurrence: true,
    RecurrenceRule: "FREQ=WEEKLY;BYDAY=MO,WE;INTERVAL=1;COUNT=6"
}, { Id: 805,
    Subject: "Intro to Computers(lecture)",
    StartTime: new Date().setHours(15, 35),
    EndTime: new Date().setHours(16, 25),
    AllDay: false,
    Recurrence: true,
    RecurrenceRule: "FREQ=WEEKLY;BYDAY=TU,TH;INTERVAL=1;COUNT=6"
}, { Id: 806,
    Subject: "Basic Carrier Development",
    StartTime: new Date().setHours(16, 0),
    EndTime: new Date().setHours(17, 15),
    AllDay: false,
    Recurrence: true,
    RecurrenceRule: "FREQ=WEEKLY;BYDAY=MO,WE;INTERVAL=1;COUNT=6"
}, { Id: 807,
    Subject: "InterPersonal Comm.",
    StartTime: new Date().setHours(17, 45),
    EndTime: new Date().setHours(19, 0),
    AllDay: false,
    Recurrence: true,
    RecurrenceRule: "FREQ=WEEKLY;BYDAY=TU,TH;INTERVAL=1;COUNT=6"
} ];

window.Keyboard = [{ Id: 900,
    Subject: "Review lesson plans for the day",
    StartTime: new Date().setHours(9, 0),
    EndTime: new Date().setHours(9, 30),
    AllDay: false,
    Recurrence: true,
    RecurrenceRule: "FREQ=DAILY;INTERVAL=1;COUNT=5"
}, { Id: 901,
    Subject: "Philosophy",
    StartTime: new Date().setHours(9, 30),
    EndTime: new Date().setHours(10, 0),
    AllDay: false,
    Recurrence: true,
    RecurrenceRule: "FREQ=WEEKLY;BYDAY=TU,WE,TH;INTERVAL=1;COUNT=9"
}, { Id: 902,
    Subject: "Grammer with sharley English",
    StartTime: new Date().setHours(10, 0),
    EndTime: new Date().setHours(11, 0),
    AllDay: false,
    Recurrence: true,
    RecurrenceRule: "FREQ=MONTHLY;BYDAY=WE;BYSETPOS=1;COUNT=6"
}, { Id: 903,
    Subject: "Lunch",
    StartTime: new Date().setHours(13, 0),
    EndTime: new Date().setHours(14, 0),
    AllDay: false,
    Recurrence: true,
    RecurrenceRule: "FREQ=WEEKLY;BYDAY=MO,TU,WE,TH,FR;INTERVAL=1;COUNT=10"
}, { Id: 904,
    Subject: "Prayer",
    StartTime: new Date().setHours(8, 0),
    EndTime: new Date().setHours(8, 15),
    AllDay: false,
    Recurrence: true,
    RecurrenceRule: "FREQ=DAILY;INTERVAL=1;COUNT=20"
}];

window.Localization = [{ Id: 1000,
    Subject: "Men's Prelim. Round SVK - SLO",
    StartTime: new Date().setHours(12, 0),
    EndTime: new Date().setHours(13, 0),
    AllDay: false,
    Recurrence: false
},
{ Id: 1001,
    Subject: "Women's Play-offs Quarterfinals",
    StartTime: new Date().setHours(16, 30),
    EndTime: new Date().setHours(18, 0),
    AllDay: false,
    Recurrence: false
}, { Id: 1002,
    Subject: "Women's Prelim. Round JPN - GER",
    StartTime: new Date(new Date().getTime() + 86400 * 1000 * -3).setHours(12, 0),
    EndTime: new Date(new Date().getTime() + 86400 * 1000 * -3).setHours(14, 0),
    AllDay: false,
    Recurrence: false
}, { Id: 1003,
    Subject: "Men's Prelim. Round FIN - AUT",
    StartTime: new Date(new Date().getTime() + 86400 * 1000 * -2).setHours(16, 30),
    EndTime: new Date(new Date().getTime() + 86400 * 1000 * -2).setHours(18, 15),
    AllDay: false,
    Recurrence: false
}, { Id: 1004,
    Subject: "Women's Preim. Round SUI -FIN",
    StartTime: new Date(new Date().getTime() + 86400 * 1000 * -2).setHours(12, 0),
    EndTime: new Date(new Date().getTime() + 86400 * 1000 * -2).setHours(14, 0),
    AllDay: false,
    Recurrence: false
}, { Id: 1005,
    Subject: "Men's Prelim Round RUS - SVK",
    StartTime: new Date(new Date().getTime() + 86400 * 1000 * 1).setHours(16, 0),
    EndTime: new Date(new Date().getTime() + 86400 * 1000 * 1).setHours(18, 15),
    AllDay: false,
    Recurrence: false
}, { Id: 1006,
    Subject: "Women's Classifications RUS - JPN",
    StartTime: new Date(new Date().getTime() + 86400 * 1000 * 1).setHours(21, 0),
    EndTime: new Date(new Date().getTime() + 86400 * 1000 * 1).setHours(22, 0),
    AllDay: false,
    Recurrence: false
}, { Id: 1007,
    Subject: "Men's Play-offs Semifinals CAN - SUI",
    StartTime: new Date(new Date().getTime() + 86400 * 1000 * 2).setHours(16, 30),
    EndTime: new Date(new Date().getTime() + 86400 * 1000 * 2).setHours(18, 0),
    AllDay: false,
    Recurrence: false
}, { Id: 1008,
    Subject: "Women's Play-offs Semifinals USA - SWE",
    StartTime: new Date(new Date().getTime() + 86400 * 1000 * 2).setHours(21, 0),
    EndTime: new Date(new Date().getTime() + 86400 * 1000 * 2).setHours(22, 0),
    AllDay: false,
    Recurrence: false
}, { Id: 1009,
    Subject: "Finals SUI - SWE",
    StartTime: new Date(new Date().getTime() + 86400 * 1000 * 3).setHours(16, 0),
    EndTime: new Date(new Date().getTime() + 86400 * 1000 * 3).setHours(18, 0),
    AllDay: false,
    Recurrence: false
}];

window.RTL = [{ Id: 1100,
    Subject: "Chemistry I (lecture)",
    StartTime: new Date().setHours(9, 0),
    EndTime: new Date().setHours(9, 55),
    AllDay: false,
    Recurrence: true,
    RecurrenceRule: "FREQ=WEEKLY;BYDAY=MO,TU,WE,FR;INTERVAL=1;COUNT=12"
},
{ Id: 1101,
    Subject: "Elementary Composition",
    StartTime: new Date().setHours(11, 15),
    EndTime: new Date().setHours(12, 05),
    AllDay: false,
    Recurrence: true,
    RecurrenceRule: "FREQ=DAILY;INTERVAL=2;COUNT=12"
},
{ Id: 1102,
    Subject: "Managing Resources for learning",
    StartTime: new Date(new Date().getTime() + 86400 * 1000 * 1).setHours(11, 15),
    EndTime: new Date(new Date().getTime() + 86400 * 1000 * 1).setHours(12, 05),
    AllDay: false,
    Recurrence: true,
    RecurrenceRule: "FREQ=DAILY;INTERVAL=2;COUNT=12"
},
{ Id: 1103,
    Subject: "Calculus I (lecture)",
    StartTime: new Date().setHours(13, 25),
    EndTime: new Date().setHours(14, 15),
    AllDay: false,
    Recurrence: true,
    RecurrenceRule: "FREQ=WEEKLY;BYDAY=MO,WE,TH,FR;INTERVAL=1;COUNT=12"
},
{ Id: 1104,
    Subject: "Chemistry I (lab)",
    StartTime: new Date().setHours(14, 30),
    EndTime: new Date().setHours(17, 30),
    AllDay: false,
    Recurrence: true,
    RecurrenceRule: "FREQ=WEEKLY;BYDAY=TH;INTERVAL=1;COUNT=4"
},
{ Id: 1105,
    Subject: "Chemistry I (discussion)",
    StartTime: new Date().setHours(16, 40),
    EndTime: new Date().setHours(17, 30),
    AllDay: false,
    Recurrence: true,
    RecurrenceRule: "FREQ=WEEKLY;BYDAY=WE;INTERVAL=1;COUNT=4"
}];
