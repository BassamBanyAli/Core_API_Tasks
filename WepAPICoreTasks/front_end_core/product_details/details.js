async function fetchProductDetails() {

    let ProductID = Number(localStorage.getItem('ProductID'));
    const url = `https://localhost:7167/api/Products/GetProductByID?id=${ProductID}`;



    const response = await fetch(url);
    const data = await response.json();
    var container=document.getElementById("container1");

    

    localStorage.setItem('ProductID',ProductID);
    

container.innerHTML=`
<div class="card" style="width: 18rem;">
  <img class="card-img-top" src="https://images.deliveryhero.io/image/talabat/MenuItems/Chicken_shawerma_638297709308038968.jpeg" alt="Card image cap">
  <div class="card-body">
    <h5 class="card-title">${data.productName}</h5>
    <p class="card-text">${data.description}</p>
   <label for="quantityInput">Quantity:</label>
     <input type="number" id="quantityInput" class="form-control" min="1" value="1">
    <a href="../Cart/Cart.html"  onclick="AddToCart()"class="btn btn-primary">Add To Card</a>
  </div>
</div>`;


};



async function AddToCart() {
  var url='https://localhost:7167/api/CartItems';

  var quantityInput = document.getElementById("quantityInput").value;
  let ProductID = Number(localStorage.getItem('ProductID'));
  var cardId=localStorage.getItem("cardId");
  var productcart={
    cartId:cardId,
    productId:ProductID,
    quantity:quantityInput
  }
  localStorage.setItem('productcart', JSON.stringify(productcart));
  var productcart=localStorage.getItem('productcart');

  var product=JSON.parse(productcart);
  var response=await fetch(url,{
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(product), 
  });
   var data=response.json();
  
}
fetchProductDetails();