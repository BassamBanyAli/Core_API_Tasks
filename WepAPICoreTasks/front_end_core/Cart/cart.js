async function fetchCart() {
  var url='https://localhost:7167/api/CartItems';
  var response=await fetch(url);
  data= await response.json();
  var cartId=data[0].CartId;
  localStorage.setItem("cardId",JSON.stringify(cartId));
  console.log(data);
  data.forEach(element => {
   


var container=document.getElementById("container");
container.innerHTML+=`<div class="row mb-4 d-flex justify-content-between align-items-center">
                         <div class="col-md-2 col-lg-2 col-xl-2">
                           <img
                             src="https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-shopping-carts/img5.webp"
                             class="img-fluid rounded-3" alt="Cotton T-shirt">
                         </div>
                         <div class="col-md-3 col-lg-3 col-xl-3">
                           <h6 class="mb-0">${element.product.productName}</h6>
                         </div>
                         <div class="col-md-3 col-lg-3 col-xl-2 d-flex">
     
                           <input id="form1" min="0" name="quantity" value="${element.quantity}" type="number"
                             class="form-control form-control-sm" data-product-id="${element.product.productId}" onchange="updateQuantity(this)"  />
     

                         </div>
                         <div class="col-md-3 col-lg-2 col-xl-2 offset-lg-1">
                           <h6 class="mb-0">${element.product.price} $</h6>
                         </div>
                           <div class="col-md-1 col-lg-1 col-xl-1 text-end">
                   <button class="btn btn-link text-muted" onclick="removeItem(${element.product.productId})">
                       <i class="fas fa-times"></i>
                   </button>
               </div>
                       </div>
     
                       <hr class="my-4">`


  
                   });
}
async function updateQuantity(input)  {
 const newQuantity = input.value;
 const productId = input.getAttribute('data-product-id');
 var url=`https://localhost:7167/GetTheProductId/${productId}`;
 var productcart={
   cartId:1,
   quantity:newQuantity
 }
 var response= await fetch(url,{
   method:"PUT",
   headers: {
     "Content-Type": "application/json",
   },
   body: JSON.stringify(productcart), 
 });
 var data= await response.json()
 debugger;


 
}

 

async function removeItem(productId) {
 var url=`https://localhost:7167/GetProductId/${productId}`;
 var response=await fetch(url,{
   method:"DELETE"
 });
 data =await response.json();
}
fetchCart();