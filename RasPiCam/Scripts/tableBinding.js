function drawTable(data, tableId, timeStampFormat) {
    for (var i = 0; i < data.length; i++) {
        drawRow(data[i], tableId, timeStampFormat);
    }
}

function deleteVideo(id) {
    $.ajax({
        url: "/api/VideoApi/Delete",
        data : { name : id },
        type : "post",
        success: function (data) {
            if (!data.Result) {
                alert(data.ExceptionMessage);
            } else {
                $("tr#" + id).remove(); // Better than querying the blob again
            }
        }
    });
}

function drawRow(rowData, tableId, timeStampFormat) {
    var row = $("<tr id=\"" + rowData.EncodedFilename + "\"/>");
    $(tableId).append(row);
    row.append($("<td width=\"10%\">" + rowData.Size + "</td>"));
    row.append($("<td width=\"60%\">" + moment(rowData.Timestamp).format(timeStampFormat) + "</td>"));
    row.append($("<td><a class=\"btn btn-default \" href=\"/Default/Video/" + rowData.EncodedFilename + "\">Download</a>&nbsp;" +
        "<a class=\"btn btn-default \" href=\"javascript:deleteVideo('" + rowData.EncodedFilename + "')\">Delete</a>" + "</td>"));
}

function populateTable(data, tableId, timeStampFormat) {
    data.sort(function (a, b) {
        return (a["Timestamp"] > b["Timestamp"]) ? 1 : ((a["Timestamp"] < b["Timestamp"]) ? -1 : 0);
    });
    drawTable(data, tableId, timeStampFormat);
}