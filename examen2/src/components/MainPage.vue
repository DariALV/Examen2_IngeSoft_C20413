<template>
  <div class="body-page">
    <div class="content-body">
      <div class="coffees">
        <div v-for="(coffee, index) in coffees" :key="index" :class="coffee.class">
          <div class="img-container">
            <img :src="coffee.image" alt="">
          </div>
          <div class="coffee-info">
            <span class="coffee-name">{{ coffee.name }}</span>
            <span class="coffee-price">₡{{ coffee.price }}</span>
            <form action="">
              <input type="number" name="" id="" :placeholder="coffee.quantity">
            </form>
            <button>Agregar al carrito</button>
          </div>
        </div>
      </div>
      <div class="checkout">
        <p class="total-title">Total</p>
        <p class="total-amount">₡13.500</p>
        <hr>
        <p class="money-title">Dinero para realizar el pago</p>
        <div class="currencies">
          <div v-for="(currency, index) in currencies" :key="index" :class="currency.class">
            <div class="currency-img">
              <img :src="currency.image" alt="">
            </div>
            <form action="">
              <input type="number" name="" id="">
            </form>
            <p class="currency-total">
              ₡{{ currency.value * currency.quantity }}
            </p>
          </div>
        </div>
        <hr>
        <p class="currency-total-amount">Faltan ₡3.500</p>
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
          {name: "Capuchino", quantity: 10, price: 950, class: "american-coffee coffee", image: require("../assets/Capuchino.png")},
          {name: "Late", quantity: 10, price: 950, class: "american-coffee coffee", image: require("../assets/Late.png")},
          {name: "Mocachino", quantity: 10, price: 950, class: "american-coffee coffee", image: require("../assets/Mocachino.png")}
        ],
        currencies: [
          {value: 1000, quantity: 5, class: "currency thousand", image: require("../assets/Billete1000.png")},
          {value: 500, quantity: 0, class: "currency five-hundred", image: require("../assets/Moneda500.png")},
          {value: 100, quantity: 0, class: "currency hundred", image: require("../assets/Moneda100.png")},
          {value: 50, quantity: 0, class: "currency fifty", image: require("../assets/Moneda50.png")},
          {value: 25, quantity: 0, class: "currency twenty-five", image: require("../assets/Moneda25.png")},
        ],
        currencyChange: [
          {value: 1000, quantity: 20}
        ]
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

<style>

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
  height: 90%;
  margin: 0 10px;
}

.coffees {
  width: 100%;
  height: 100%;
}

.coffee {
  background-color: white;
  margin: 10px 5px;
  display: flex;
  flex-direction: row;
  width: 100%;
  height: 23.5%;
  border-radius: 14px;
  box-shadow: 0px 0px 5px 0px rgba(0, 0, 0, 0.25);
}

.coffee-info {
  width: 100%;
  display: grid;
  grid-template-columns: 1fr 1fr;
  grid-template-rows: 1fr 1fr;
  padding: 20px;
}

.coffee-name {
  color: #000;
  font-feature-settings: 'liga' off, 'clig' off;
  font-size: 30px;
  font-style: normal;
  font-weight: 600;
  line-height: 130%; /* 39px */
  justify-self: start;
  align-self: start;
}

.coffee-price {
  color: #C0702A;
  text-align: right;
  font-feature-settings: 'liga' off, 'clig' off;
  font-size: 30px;
  font-style: normal;
  font-weight: 500;
  line-height: 130%; /* 39px */
  justify-self: end;
  align-self: start;
}

.coffee form {
  justify-self: start;
  align-self: end;
}

.coffee button {
  width: 229px;
  height: 55px;
  flex-shrink: 0;
  background: #C0702A;
  border: transparent;
  color: #FFF;
  text-align: center;
  font-feature-settings: 'liga' off, 'clig' off;
  font-size: 20px;
  font-style: normal;
  font-weight: 600;
  line-height: 130%; /* 26px */
  justify-self: end;
  align-self: end;
}

.img-container img {
  width: 100%;
  height: 100%;
}

.checkout {
  grid-column: 2/3;
  background-color: white;
  padding: 10px;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  margin: 10px 0;
  border-radius: 14px;
  box-shadow: 0px 0px 5px 0px rgba(0, 0, 0, 0.25);
}

.currencies {
  display: grid;
  grid-template-rows: 1fr 1fr 1fr 1fr 1fr;
  padding: 20px;
}

.currency {
  display: grid;
  grid-template-columns: 1fr 1fr 1fr;
  width: 450px;
}

.currency form {
  align-self: center;
  justify-self: center;
}

.total-title, .money-title {
  color: #000;
  text-align: center;
  font-feature-settings: 'liga' off, 'clig' off;
  font-family: Poppins;
  font-size: 30px;
  font-style: normal;
  font-weight: 600;
  line-height: 130%; /* 39px */
}

.currency-total, .total-amount {
  color: #C0702A;
  text-align: right;
  font-feature-settings: 'liga' off, 'clig' off;
  font-size: 30px;
  font-style: normal;
  font-weight: 500;
  line-height: 130%; /* 39px */
  justify-self: start;
  align-self: center;
}

.currency-total {
  justify-self: start;
  margin-left: 20px;
  align-self: center;
}

.currency-img {
  width: 121px;
  display: flex;
  align-items: center;
  justify-content: center;
}

hr {
  border: 1px solid rgb(209, 209, 209);
  border-radius: 5px;
  width: 100%;
}

.currency-total-amount {
  color: #ff0000;
  text-align: right;
  font-feature-settings: 'liga' off, 'clig' off;
  font-size: 24px;
  font-style: normal;
  font-weight: 500;
  line-height: 130%;
}

.checkout button {
  width: 350px;
  height: 70px;
  flex-shrink: 0;
  background: #C0702A;
  border: transparent;
  color: #FFF;
  text-align: center;
  font-feature-settings: 'liga' off, 'clig' off;
  font-size: 28px;
  font-style: normal;
  font-weight: 600;
  line-height: 130%; /* 26px */
  justify-self: center;
}
</style>