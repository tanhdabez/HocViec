document.getElementById('trangThaiSwitch').addEventListener('change', function () {
    this.nextElementSibling.textContent = this.checked ? 'Hoạt động' : 'Không hoạt động';
});

document.addEventListener('DOMContentLoaded', function () {
    let selectedFiles = []; // Mảng để lưu trữ các file đã chọn

    document.getElementById('imageInput').addEventListener('change', function () {
        selectedFiles = Array.from(this.files); // Lưu trữ các file đã chọn vào mảng
        const preview = document.getElementById('imagePreview');
        preview.innerHTML = ''; // Xóa ảnh xem trước cũ

        selectedFiles.forEach(file => {
            const reader = new FileReader();
            reader.onload = function (e) {
                const imgContainer = document.createElement('div');
                imgContainer.style.display = 'inline-block';

                const img = document.createElement('img');
                img.src = e.target.result;
                img.style.maxWidth = '100px';
                img.style.marginRight = '5px';

                const removeButton = document.createElement('button');
                removeButton.textContent = 'Xóa';
                removeButton.className = 'btn btn-danger btn-sm';
                removeButton.style.marginLeft = '5px';

                removeButton.addEventListener('click', function () {
                    const index = selectedFiles.indexOf(file);
                    if (index !== -1) {
                        selectedFiles.splice(index, 1); // Xóa file khỏi mảng
                        preview.removeChild(imgContainer); // Xóa ảnh xem trước
                    }
                });

                imgContainer.appendChild(img);
                imgContainer.appendChild(removeButton);
                preview.appendChild(imgContainer);
            };
            reader.readAsDataURL(file);
        });
    });

    // Trước khi submit form, gán lại mảng cho this.files
    document.querySelector('form').addEventListener('submit', function (event) {
        const dataTransfer = new DataTransfer();
        selectedFiles.forEach(file => {
            dataTransfer.items.add(file);
        });
        document.getElementById('imageInput').files = dataTransfer.files;
    });
    document.addEventListener('DOMContentLoaded', function () {
        const imageInput = document.getElementById('imageInput');
        const imagePreview = document.getElementById('imagePreview');
        const deletedImages = []; 
        imageInput.addEventListener('change', function () {
            imagePreview.innerHTML = ''; // Xóa ảnh xem trước cũ

            Array.from(this.files).forEach(file => {
                const reader = new FileReader();
                reader.onload = function (e) {
                    const imgContainer = document.createElement('div');
                    imgContainer.style.display = 'inline-block';

                    const img = document.createElement('img');
                    img.src = e.target.result;
                    img.style.maxWidth = '100px';
                    img.style.marginRight = '5px';

                    const removeButton = document.createElement('button');
                    removeButton.textContent = 'Xóa';
                    removeButton.className = 'btn btn-danger btn-sm';
                    removeButton.style.marginLeft = '5px';

                    removeButton.addEventListener('click', function () {
                        imagePreview.removeChild(imgContainer); // Xóa ảnh xem trước
                    });

                    imgContainer.appendChild(img);
                    imgContainer.appendChild(removeButton);
                    imagePreview.appendChild(imgContainer);
                };
                reader.readAsDataURL(file);
            });
        });

    });
});
async function deleteImage(imageUrl) {
    if (confirm("Bạn có chắc chắn muốn xoá ảnh này không?")) {
        try {
            // Gửi yêu cầu xoá ảnh lên server
            const response = await fetch(`/SanPham/DeleteImage?imageUrl=${encodeURIComponent(imageUrl)}`, {
                method: 'POST',
            });

            if (response.ok) {
                // Xoá ảnh khỏi giao diện nếu xoá thành công
                const imageContainer = document.querySelector(`.image-container img[src="${imageUrl}"]`).parentElement;
                imageContainer.remove();
            } else {
                alert("Xoá ảnh thất bại. Vui lòng thử lại.");
            }
        } catch (error) {
            console.error("Lỗi khi xoá ảnh:", error);
            alert("Đã xảy ra lỗi khi xoá ảnh.");
        }
    }
}