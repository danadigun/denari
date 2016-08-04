$('#requestDemo').click(function () {
    $.ajax({
        type: 'POST',
        url: 'https://mandrillapp.com/api/1.0/messages/send.json',
        data: {
            'key': 'Q9V8aMb3pLwJB84giUq1zQ',
            'message': {
                'from_email': 'daniel.adigun@digitalforte.ng',
                'to': [{
                    'email': $('#email').val(), // get email from form
                    'name': $('#name').val(), // get name from form
                    'type': 'to'
                }
                  , {
                      'email': 'daniel.adigun@digitalforte.ng',
                      'name': '',
                      'type': 'to'
                  }
                ],
                // optional merge variables. must also be setup on the list management side of mandrill
                //'merge_vars': [{
                //    'rcpt': $('.email').val(),
                //    'vars': [{
                //        'name': 'COOLFRIEND',
                //        "content": 'Mike'
                //    }, {
                //        'name': 'YEARS',
                //        'content': '27'
                //    }]
                //}],
                'autotext': 'true',
                'subject': 'Demo Access Request for Denari - Welcome to Denari',
                'html': "Welcome to Denari. One of Africa's largest lending management platforms for Cooperatives and lending orgnizations ", // example of how to use the merge tags
                'track_opens': true,
                'track_clicks': true,
            }
        }
    }).done(function (response) {
        alert("Thank you for your interest in Denari")
        console.log(response); // if you're into that sorta thing
    });
});