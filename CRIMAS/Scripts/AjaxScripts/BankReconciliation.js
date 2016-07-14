/// <reference path="../jquery.validate.js" />
///BankReconciliation.js
$(document).ready(function (event) {

    //preloader
    $('#reconciliation_loader').hide();

    //add new reconciliation Properties
    $("#update_properties").click(function (event) {

        //event.preventDefault();

        //validate form
        $('#update_properties_form').validate({
            submitHandler: function (form) {
                // do other things for a valid form
                //form.submit();
                $('#reconciliation_loader').show();
                var properties = {
                    int_cap_value: $('input[name=int_cap_value]').val(),
                    transfer_fee: $('input[name=transfer_fee]').val(),
                    vat: $('input[name=vat]').val(),
                    sms_charge: $('input[name=sms_charge]').val(),
                    per_capital_commission: $('input[name=per_capital_commission]').val()
                };

                $.ajax({
                    type: "POST",
                    url: "/ReconcilliationSettings/Create",
                    data: properties,

                })
                .done(function (data) {
                    $('#reconciliation_loader').hide();
                    Materialize.toast('You have successfully updated bank reconciliation properties', 4000);
                })
                .fail(function (xhr, textStatus) {
                    alert('Error: unable to update records');
                    console.log(xhr)
                })
                .always(function (result) { console.log('Request done!') });
            }
        });

      

    })

})

