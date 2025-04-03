$(document).ready(function () {
    let typingTimer;

    function loadPage(pageNumber, pageSize) {
        $.ajax({
            url: '/HoaDon/GetHoaDonPage',
            type: 'GET',
            data: {
                pageNumber: pageNumber,
                pageSize: pageSize,
                startDate: $('#startDate').val(),
                endDate: $('#endDate').val(),
                trangThai: $('#statusFilter').val(),
                maHD: $('#invoiceCode').val()
            },
            success: function (data) {
                $('#hoaDonTableContainer').html(data);
            },
            error: function (error) {
                console.error('Lỗi khi tải trang:', error);
            }
        });
    }

    // Thiết lập giá trị mặc định cho ngày
    let today = new Date();
    let oneWeekAgo = new Date(today);
    oneWeekAgo.setDate(today.getDate() - 7);

    $('#startDate').val(oneWeekAgo.toISOString().split('T')[0]);
    $('#endDate').val(today.toISOString().split('T')[0]);

    // Gán sự kiện lọc dữ liệu
    $('#statusFilter, #filterButton').on('change click', function () {
        loadPage(1);
    });

    $('#invoiceCode').on('input', function () {
        clearTimeout(typingTimer);
        typingTimer = setTimeout(() => loadPage(1), 300);
    });
});
