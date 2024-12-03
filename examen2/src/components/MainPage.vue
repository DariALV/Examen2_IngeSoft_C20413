<template>
  <div class="body-page">
    <div class="content-body">
      <div class="coffees">
        <div class="american-coffee coffee">
          <div class="img-container">
            <img src="../assets/Cafe_Americano.png" alt="">
          </div>
          <span>Café americano</span>
          <span>₡950</span>
          <form action="">
            <input type="number" name="" id="">
          </form>
          <button>Agregar al carrito</button>
        </div>
        <div class="capuchino coffee">
          <div class="img-container">
            <img src="../assets/Capuchino.png" alt="">
          </div>
          <span>Café americano</span>
          <span>₡950</span>
          <form action="">
            <input type="number" name="" id="">
          </form>
          <button>Agregar al carrito</button>
        </div>
        <div class="late coffee">
          <div class="img-container">
            <img src="../assets/Late.png" alt="">
          </div>
          <span>Café americano</span>
          <span>₡950</span>
          <form action="">
            <input type="number" name="" id="">
          </form>
          <button>Agregar al carrito</button>
        </div>
        <div class="american-coffee coffee">
          <div class="img-container">
            <img src="../assets/Mocachino.png" alt="">
          </div>
          <span>Café americano</span>
          <span>₡950</span>
          <form action="">
            <input type="number" name="" id="">
          </form>
          <button>Agregar al carrito</button>
        </div>
      </div>
      <div class="checkout">
        <p class="total-title">Total</p>
        <p class="total-amount">₡13.500</p>
        <hr>
        <p class="money-title">Dinero para realizar el pago</p>
        <div class="currencies">
          <div class="currency thousand">
            <div class="img-container currency-img">
              <img src="" alt="">
            </div>
            <form action="">
              <input type="number" name="" id="">
            </form>
            <p class="currency-total">
              ₡6.000
            </p>
          </div>
          <div class="currency five-hundred">
            <div class="img-container currency-img">
              <img src="" alt="">
            </div>
            <form action="">
              <input type="number" name="" id="">
            </form>
            <p class="currency-total">
              ₡5.000
            </p>
          </div>
          <div class="currency hundred">
            <div class="img-container currency-img">
              <img src="" alt="">
            </div>
            <form action="">
              <input type="number" name="" id="">
            </form>
            <p class="currency-total">
              ₡1.000
            </p>
          </div>
          <div class="currency fifty">
            <div class="img-container currency-img">
              <img src="" alt="">
            </div>
            <form action="">
              <input type="number" name="" id="">
            </form>
            <p class="currency-total">
              ₡1.500
            </p>
          </div>
          <div class="currency twenty-five">
            <div class="img-container currency-img">
              <img src="" alt="">
            </div>
            <form action="">
              <input type="number" name="" id="">
            </form>
            <p class="currency-total">
              ₡0
            </p>
          </div>
        </div>
        <hr>
        <p class="currency-total-amount">₡13.500</p>
        <button>Realizar la compra</button>
      </div>
    </div>
  </div>
</template>

<script>
  export default {
    name: 'MainPage'
  }
</script>

<style scoped>

  @import url('https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;500;600;800&display=swap');
  
  *{
    margin: 0;
    padding: 0;
    box-sizing: border-box;
    font-family: 'poppins', sans-serif;
  }

.body-page {
  display: flex;
  justify-content: center;
  align-items: center;
  background-color: #F3F4F6;
  height: 100vh;
  width: 100vw;
}

.content-body {
  display: grid;
  grid-template-columns: 1fr 1fr;
  grid-template-rows: 1fr;
  column-gap: 20px;
  width: 100%;
  margin: 0 10px;
}

.coffees {
  width: 100%;
}

.coffee {
  background-color: white;
  margin: 10px 5px;
  display: flex;
  flex-direction: row;
  width: 100%;
}

.img-container {
  height: 100px;
}

.checkout {
  grid-column: 2/3;
  background-color: white;
  padding: 10px;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}
</style>