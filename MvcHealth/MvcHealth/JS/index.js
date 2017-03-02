function getID(checkId) {
    $.ajax({
        url: '/Home/getHealthData',
        type: 'POST',
        contentType: 'application/json;',
        data: JSON.stringify({ id: checkId }),
        success: function (data) {
            $("#myModal").modal('show');
            $("#subj_id").val(data.ID);
            $("#DRG_Definition").val(data.DRG_Definition);
            $("#Provider_Id").val(data.Provider_Id);
            $("#Provider_Name").val(data.Provider_Name);
            $("#Provider_Street_Address").val(data.Provider_Street_Address);
            $("#Provider_City").val(data.Provider_City);
            $("#Provider_State").val(data.Provider_State);
            $("#Provider_Zip_Code").val(data.Provider_Zip_Code);
            $("#Hospital_Referral_Region_Description").val(data.Hospital_Referral_Region_Description);
            $("#_Total_Discharges_").val(data._Total_Discharges_);
            $("#_Average_Covered_Charges_").val(data._Average_Covered_Charges_);
            $("#_Average_Total_Payments_").val(data._Average_Total_Payments_);
        }
    });
}

function callDeleteModal(checkId) {
    $("#deleteModal").modal('show');
    $("#del_subj_id").val(checkId);
}


function backHome() {
    window.history.back();
}