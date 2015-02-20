function drawTable(data, tableId, timeStampFormat) {
    for (var i = 0; i < data.length; i++) {
        drawRow(data[i], tableId, timeStampFormat, i);
    }
}

function appendErrorRow(rowId, title, message) {
    var row = $("tr#" + rowId);
    var errorId = "error-" + rowId;
    row.after("<tr id=\"" + errorId + "\"><td colspan=\"3\"><div class=\"alert alert-danger col-md-12\"><strong>" + title + ":</strong> " + message + "</div></td></tr>");
    $("tr#" + errorId).click(function(sender) {
        $("tr#" + errorId).remove();
    });

}

function deleteVideo(id, rowId) {
    $.ajax({
        url: "/api/VideoApi/Delete",
        data : { name : id },
        type : "post",
        success: function (data) {
            if (data.Result) {
                $("tr#" + rowId).remove(); // Better than querying the blob again
            } else {
                appendErrorRow(rowId, data.ExceptionClass, data.ExceptionMessage);
            }
        },
        error: function (jqXhr, textStatus, errorThrown) {
            appendErrorRow(rowId, "Error", textStatus);
        }
    });
}

function drawRow(rowData, tableId, timeStampFormat, rowId) {
    var id = tableId + "-" + rowId;
    var downloadId = "download-" + id;
    var deleteId = "delete-" + id;

    var row = $("<tr id=\"" + id + "\"/>");
    $("#" + tableId).append(row);
    row.append($("<td width=\"10%\">" + rowData.Size + "</td>"));
    row.append($("<td width=\"10%\">" + moment.duration(rowData.Metadata.duration).as("seconds") + " seconds</td>"));
    row.append($("<td width=\"60%\">" + moment(rowData.Timestamp).format(timeStampFormat) + "</td>"));
    row.append($("<td><div class=\"btn btn-default\" id=\""+ downloadId + "\">Download</div>&nbsp;" +
        "<div class=\"btn btn-default\" id=\"" + deleteId + "\">Delete</div>" + "</td>"));

    $("div#" + downloadId).click(function() {
        document.location = "/Default/Video/" + rowData.EncodedFilename;
    });

    $("div#" + deleteId).click(function () {
        deleteVideo(rowData.EncodedFilename, id);
    });
}

function populateTable(data, tableId, timeStampFormat) {
    data.sort(function (a, b) {
        return (a["Timestamp"] > b["Timestamp"]) ? 1 : ((a["Timestamp"] < b["Timestamp"]) ? -1 : 0);
    });
    drawTable(data, tableId, timeStampFormat);
}