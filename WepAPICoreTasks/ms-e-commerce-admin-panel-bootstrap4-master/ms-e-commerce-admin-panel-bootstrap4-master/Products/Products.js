async function fetchAndDisplayCards() {

    const url = 'https://localhost:44323/api/Products';
    const response = await fetch(url);
    const data = await response.json();
    console.log(data);
    var tbody = document.querySelector("#datatable2 tbody");
    data.forEach(element => {
        
        var row = `
        <tr>
            <td>${element.productName}</td>
            <td>${element.description}</td>
            <td>${element.price}</td>
            <td>${element.categoryId}</td>
            <td><img src="${element.productImage}" alt="Image" style="width:50px;height:50px;"></td> 
            <td align="center">
                <div class="dropdown">
                    <a class="la la-ellipsis-h dropdown-toggle" id="dropdownMenuButton${element.categoryId}" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></a>
                    <div class="dropdown-menu" aria-labelledby="dropdownMenuButton${element.categoryId}">
                        <a class="dropdown-item" href="#"><i class="la la-info-circle"></i> View Details</a>
                        <div class="dropdown-divider"></div>
                        <a class="dropdown-item" href="#"><i class="la la-paperclip"></i> Create Product</a>
                        <a class="dropdown-item" onclick="navigateEdit(${element.categoryId})" href="edit.html"><i class="la la-cog"></i> Edit Product</a>
                        <a class="dropdown-item" href="#"><i class="la la-cloud-download"></i> View Details</a>
                    </div>
                </div>
            </td>
        </tr>`;
        tbody.innerHTML += row;
    });
}

fetchAndDisplayCards();

function navigate() {
    window.location.href = "Create.html";
}

function navigateEdit(value){
    localStorage.setItem('EditID',value);
    window.location.href="Edit.html";
}
