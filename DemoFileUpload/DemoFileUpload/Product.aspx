<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="DemoFileUpload.Product" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Clothing Store</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" />

</head>
<body>
    
    <head>
    <meta charset="utf-8">
    <title>EShopper - Bootstrap Shop Template</title>
    <meta content="width=device-width, initial-scale=1.0" name="viewport">
    <meta content="Free HTML Templates" name="keywords">
    <meta content="Free HTML Templates" name="description">

    <!-- Favicon -->
    <link href="img/favicon.ico" rel="icon">

    <!-- Google Web Fonts -->
    <link rel="preconnect" href="https://fonts.gstatic.com">
    <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@100;200;300;400;500;600;700;800;900&display=swap" rel="stylesheet"> 

    <!-- Font Awesome -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.10.0/css/all.min.css" rel="stylesheet">

    <!-- Libraries Stylesheet -->
    <link href="lib/owlcarousel/assets/owl.carousel.min.css" rel="stylesheet">

    <!-- Customized Bootstrap Stylesheet -->
    <link href="css/style.css" rel="stylesheet">
</head>

<body>
    <!-- Topbar Start -->
    <div class="container-fluid">
        <div class="row bg-secondary py-2 px-xl-5">
            <div class="col-lg-6 d-none d-lg-block">
                <div class="d-inline-flex align-items-center">
                    <a class="text-dark" href="">FAQs</a>
                    <span class="text-muted px-2">|</span>
                    <a class="text-dark" href="">Help</a>
                    <span class="text-muted px-2">|</span>
                    <a class="text-dark" href="">Support</a>
                </div>
            </div>
            <div class="col-lg-6 text-center text-lg-right">
                <div class="d-inline-flex align-items-center">
                    <a class="text-dark px-2" href="">
                        <i class="fab fa-facebook-f"></i>
                    </a>
                    <a class="text-dark px-2" href="">
                        <i class="fab fa-twitter"></i>
                    </a>
                    <a class="text-dark px-2" href="">
                        <i class="fab fa-linkedin-in"></i>
                    </a>
                    <a class="text-dark px-2" href="">
                        <i class="fab fa-instagram"></i>
                    </a>
                    <a class="text-dark pl-2" href="">
                        <i class="fab fa-youtube"></i>
                    </a>
                </div>
            </div>
        </div>
        <div class="row align-items-center py-3 px-xl-5">
            <div class="col-lg-3 d-none d-lg-block">
                <a href="" class="text-decoration-none">
                    <h1 class="m-0 display-5 font-weight-semi-bold"><span class="text-primary font-weight-bold border px-3 mr-1">E</span>Shopper</h1>
                </a>
            </div>
            <div class="col-lg-6 col-6 text-left">
                <form action="">
                    <div class="input-group">
                        <input type="text" class="form-control" placeholder="Search for products">
                        <div class="input-group-append">
                            <span class="input-group-text bg-transparent text-primary">
                                <i class="fa fa-search"></i>
                            </span>
                        </div>
                    </div>
                </form>
            </div>
            <div class="col-lg-3 col-6 text-right">
                <a href="" class="btn border">
                    <i class="fas fa-heart text-primary"></i>
                    <span class="badge">0</span>
                </a>
                <a href="" class="btn border">
                    <i class="fas fa-shopping-cart text-primary"></i>
                    <span class="badge">0</span>
                </a>
            </div>
        </div>
    </div>
    <!-- Topbar End -->


    <!-- Navbar Start -->
    <div class="container-fluid mb-5">
        <div class="row border-top px-xl-5">
           </div>
            <div class="col-lg-9">
                <nav class="navbar navbar-expand-lg bg-light navbar-light py-3 py-lg-0 px-0">
                    <a href="" class="text-decoration-none d-block d-lg-none">
                        <h1 class="m-0 display-5 font-weight-semi-bold"><span class="text-primary font-weight-bold border px-3 mr-1">E</span>Shopper</h1>
                    </a>
                    <button type="button" class="navbar-toggler" data-toggle="collapse" data-target="#navbarCollapse">
                        <span class="navbar-toggler-icon"></span>
                    </button>
                    <div class="collapse navbar-collapse justify-content-between" id="navbarCollapse">
                        <div class="navbar-nav mr-auto py-0">
                            <a href="index.aspx" class="nav-item nav-link active">Home</a>
                            <a href="shop.html" class="nav-item nav-link">Shop</a>
                            <a href="detail.html" class="nav-item nav-link">Shop Detail</a>
                            <div class="nav-item dropdown">
                                <a href="#" class="nav-link dropdown-toggle" data-toggle="dropdown">Pages</a>
                                <div class="dropdown-menu rounded-0 m-0">
                                    <a href="cart.html" class="dropdown-item">Shopping Cart</a>
                                    <a href="checkout.html" class="dropdown-item">Checkout</a>
                                </div>
                            </div>
                            <a href="contact.html" class="nav-item nav-link">Contact</a>
                        </div>
                        <div class="navbar-nav ml-auto py-0">
                        </div>
                    </div>
                </nav>   
                </div>
            </div>
        
 
    <!-- Navbar End -->


    <!-- Add this div at the top of the page to display cart items -->
    <div id="cartItemsDisplay" style="display: none;">
        <h3>Shopping Cart</h3>
        <ul id="cartItemListWebPage" runat="server"></ul>
    </div>

    <div id="btnDisplay" style="display: none">

        <button type="button" class="btn btn-secondary" onclick="goBack()">Back</button>
    </div>

    <form id="form1" runat="server" action="Product.aspx/ProcessCheckout" method="post">
        <asp:ScriptManager runat="server"></asp:ScriptManager>

        <div id="cart" class="container mt-4">
             <asp:Label ID="lblFullName" runat="server" Text=""></asp:Label>
            <div class="row">
                <div class="col-md-6">
                </div>
            </div>

            <!-- Add this button at the top of the page -->
        


            <!-- Cart Modal -->
            <div class="modal fade" id="cartModal" tabindex="-1" role="dialog" aria-labelledby="cartModalLabel" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="cartModalLabel">Shopping Cart</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <!-- Cart items will be displayed here -->
                            <ul id="cartItemList" runat="server"></ul>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>

            <h1>Clothing Store</h1>

        <asp:Button ID="Button2" runat="server" Text="Shop" OnClick="Button2_Click" />

            <asp:DataList ID="DataList1" runat="server" RepeatDirection="Horizontal" CellPadding="5" OnDataList1_ItemCommand="DataList1_ItemCommand" OnItemCommand="DataList1_ItemCommand" DataKeyField="ID" CellSpacing="3">
     <ItemTemplate>
         <!-- Display product details -->
         <div class="card" style="width: 18rem;">
             <div class="card-body">
                 <h5 class="card-title"><%# Eval("Name") %></h5>
                 <p class="card-text">Price: ₹ <%# Eval("Price") %></p>
                 <img id="imgProduct" runat="server" src='<%# Eval("Photo") %>' class="card-img-top img-fluid" />
                 <button CssClass="btn btn-primary">Add to cart</button>

                
             </div>
         </div>
     </ItemTemplate>
       </asp:DataList>
        </div>

        <script>
            var cartCount = 0;
            var cartItems = [];

            function addToCart(productId, productName, price) {
                cartCount++;
                cartItems.push({ productId, productName, price });
                updateCartCount();
            }

            //function updateCartCount() {
              //  document.getElementById("<% %>").innerText = cartCount;
            //}

            // Function to display cart items on the webpage
            function displayCartItems() {
                var cartItemsDisplay = document.getElementById("cartItemsDisplay");
                var btnDisplay = document.getElementById("btnDisplay");

                var productContainer = document.getElementById("productContainer");
                var backButton = document.getElementById("backButton");
                var cart = document.getElementById("cart");



                // Clear existing items
                cartItemListWebPage.innerHTML = "";

                // Display each item in the cart
                cartItems.forEach(function (item) {
                    var listItem = document.createElement("li");
                    listItem.textContent = item.productName + " - " + item.price;
                    cartItemListWebPage.appendChild(listItem);
                });

                // Show the cart items display
                cartItemsDisplay.style.display = "block";

                btnDisplay.style.display = "block";


                // Hide the product container
                productContainer.style.display = "none";

                // Show the "Back" button
                backButton.style.display = "block";


            }

            function goBack() {
                var cartItemsDisplay = document.getElementById("cartItemsDisplay");
                var productContainer = document.getElementById("productContainer");
                var backButton = document.getElementById("backButton");

                // Show the product container
                productContainer.style.display = "block";

                // Hide the cart items display
                cartItemsDisplay.style.display = "none";

                // Hide the "Back" button on the product container
                backButton.style.display = "none";
            }

            function submitForm() {
                // Assuming your form has the ID "form1
                var form = document.getElementById("form1")

                form.submit();
            }

           






        </script>
    </form>
</body>
</html>
