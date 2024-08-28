async function fetchAndDisplayCards() {

        const url = 'https://localhost:44323/api/Categories';
        const response = await fetch(url);
        const data = await response.json();
        console.log(data);
        var tbody = document.querySelector("#datatable2 tbody");
        data.forEach(element => {
            
        
        var row= `
        <tr>
            <td>${element.categoryName}</td> <!-- Assuming 'name' is the category name field -->
            <td><img src="${element.categoryImage}" alt="Image" style="width:50px;height:50px;"></td> <!-- Assuming 'imageUrl' is the image field -->
            <td align="center">
                <div class="dropdown">
                    <a class="la la-ellipsis-h dropdown-toggle" id="dropdownMenuButton${element.categoryId}" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></a>
                    <div class="dropdown-menu" aria-labelledby="dropdownMenuButton${element.categoryId}">
                        <a class="dropdown-item" href="#"><i class="la la-info-circle"></i> View Details</a>
                        <div class="dropdown-divider"></div>
                        <a class="dropdown-item" href="#"><i class="la la-paperclip"></i> Create Category</a>
                        <a class="dropdown-item"onclick="navigateEdit()" href="edit.html"><i class="la la-cog"></i> Edit Category</a>
                        <a class="dropdown-item" href="#"><i class="la la-cloud-download"></i> View Details</a>
                    </div>
                </div>
            </td>
        </tr>`;
        tbody.innerHTML += row;
});


       

    }


fetchAndDisplayCards();

function navigate(){
    window.location.href="Create.html";
}
function navigateEdit(){
    localStorage.setItem('EditID', card.categoryId);
    window.location.href="edit.html";
}