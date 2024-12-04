<template>
  <div class="body-page">
    <div class="content-body">
      <div class="coffees">
        <div v-for="(coffee, index) in coffees" :key="index" :class="coffee.class">
          <div class="img-container">
            <img :src="coffee.image" alt="">
          </div>
          <span>{{ coffee.name }}</span>
          <span>₡{{ coffee.price }}</span>
          <form action="">
            <input type="number" name="" id="" :placeholder="coffee.quantity">
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

  import axios from 'axios';
  export default {
    name: 'MainPage',
    data() {
      return {
        coffees: [
          {name: "Café americano", quantity: 10, price: 950, class: "american-coffee coffee", image: require("../assets/Cafe_Americano.png")},
          {name: "Café americano", quantity: 10, price: 950, class: "american-coffee coffee", image: require("../assets/Capuchino.png")},
          {name: "Café americano", quantity: 10, price: 950, class: "american-coffee coffee", image: require("../assets/Late.png")},
          {name: "Café americano", quantity: 10, price: 950, class: "american-coffee coffee", image: require("../assets/Mocachino.png")}
        ],
      }
    },
    methods: {
      axiosTest() {
        axios.get("https://localhost:7148/WeatherForecast").then((response) => {
          console.log(response.data)
        })
      }
    },
    created() {
      this.axiosTest()
    }
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
  object-fit: contain;
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