async function fetchProductDetails() {

    let ProductID = Number(localStorage.getItem('ProductID'));
    const url = `https://localhost:44323/api/Products/GetProductByID?id=${ProductID}`;

    const response = await fetch(url);
    const data = await response.json();
    var container=document.getElementById("container1");



container.innerHTML=`
<div class="card" style="width: 18rem;">
  <img class="card-img-top" src="https://images.deliveryhero.io/image/talabat/MenuItems/Chicken_shawerma_638297709308038968.jpeg" alt="Card image cap">
  <div class="card-body">
    <h5 class="card-title">${data.productName}</h5>
    <p class="card-text">${data.description}</p>
    <a href="#" class="btn btn-primary">Add To Card</a>
  </div>
</div>`;


}
fetchProductDetails();