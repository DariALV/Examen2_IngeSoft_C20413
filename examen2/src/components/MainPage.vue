<template>
  <div class="body-page">
    <div class="content-body">
      <div class="coffees">
        <div v-for="(coffee, index) in coffees" :key="index" class="coffee">
          <div class="img-container">
            <img :src="coffeeImages[index]" alt="">
          </div>
          <div class="coffee-info">
            <span class="coffee-name">{{ coffee.name }}</span>
            <span class="coffee-price">{{ formatMoney(coffee.price) }}</span>
            <v-text-field
              :label="'Cantidad de cafés: ' + coffee.quantity.toString()"
              color="primary"
              v-model="coffeeBeforeSelection[index]"
              :rules="[validateCoffeeSelection(index)]"
              variant="outlined"
            ></v-text-field>
            <button v-if="coffee.quantity == 0" class="disabled-btn">Agregar al carrito</button>
            <button v-else-if="coffeeSelected[index] == 0" @click="setCoffeeSelection(index)" class="useful-btn">Agregar al carrito</button>
            <button v-else-if="coffeeSelected[index] > 0" @click="setCoffeeSelection(index)" class="useful-btn">Modificar producto</button>
          </div>
        </div>
      </div>
      <div class="checkout">
        <p class="total-title">Total</p>
        <p class="total-amount">{{ formatMoney(getTotalCoffeeValue()) }}</p>
        <hr>
        <p class="money-title">Dinero para realizar el pago</p>
        <div class="currencies">
          <div v-for="(currency, index) in currencies" :key="index" :class="currency.class">
            <div class="currency-img">
              <img :src="currency.image" alt="">
            </div>
            <v-text-field
              label="Cantidad"
              color="primary"
              v-model="currency.quantity"
              variant="outlined"
              maxlength="2"
              counter
            ></v-text-field>
            <p class="currency-total">
              {{ formatMoney(getIndividualCurrency(index)) }}
            </p>
          </div>
        </div>
        <hr>
        <p v-if="!isCoffeeSelected" class="currency-required-amount">Por favor, seleccione un café</p>
        <p v-else-if="getRequestedMoney() > 0" class="currency-required-amount">Faltan {{ formatMoney(getRequestedMoney()) }}</p>
        <p v-else-if="formatMoney(getRequestedMoney()) == 'Inválido'" class="currency-required-amount">Una de las monedas o billetes ingresados es inválido.</p>
        <p v-else class="currency-confirmation">Vuelto a regresar: {{ formatMoney(-getRequestedMoney()) }}</p>
        <button v-if="this.getRequestedMoney() > 0 || !this.isCoffeeSelected" class="disabled-btn">Realizar la compra</button>
        <button v-else @click="buyCoffee()" class="useful-btn">Realizar la compra</button>
      </div>
    </div>
  </div>
  <v-container>
      <v-dialog max-width="500" v-model="isBuyClicked">
        <template v-slot:default="{ isActive }">
          <v-card title="Desglose de vuelto" v-if="!errorWhenBuying">
            <v-card-text>
              <p>Su vuelto es de {{ formatMoney(requestedMoneyForChange) }}</p>
              <p>Desglose:</p>
              <div v-for="(currency, index) in usedChange" :key="index">
                <p v-if="currency.quantity > 0">{{ getCurrencyChangeTotal(currency) }}</p>
              </div>
            </v-card-text>
      
            <v-card-actions>
              <v-spacer></v-spacer>
      
              <v-btn
                text="Cerrar"
                @click="isActive.value = false"
              ></v-btn>
            </v-card-actions>
          </v-card>
          <v-card title="Error al comprar" v-else>
            <v-card-text>
              No hay suficientes monedas para regresar el cambio
            </v-card-text>
      
            <v-card-actions>
              <v-spacer></v-spacer>
      
              <v-btn
                text="Cerrar"
                @click="isActive.value = false"
              ></v-btn>
            </v-card-actions>
          </v-card>
        </template>
      </v-dialog>
    </v-container>
</template>

<script>

  import axios from 'axios';
  export default {
    name: 'MainPage',
    data() {
      return {
        coffees: [],
        coffeeImages: [require("../assets/Cafe_Americano.png"), 
                       require("../assets/Capuchino.png"), 
                       require("../assets/Late.png"), 
                       require("../assets/Mocachino.png")],
        coffeeSelected: [0, 0, 0, 0],
        coffeeBeforeSelection: ['', '', '', ''],
        currencies: [
          {value: 1000, quantity: '', class: "currency thousand", image: require("../assets/Billete1000.png")},
          {value: 500, quantity: '', class: "currency five-hundred", image: require("../assets/Moneda500.png")},
          {value: 100, quantity: '', class: "currency hundred", image: require("../assets/Moneda100.png")},
          {value: 50, quantity: '', class: "currency fifty", image: require("../assets/Moneda50.png")},
          {value: 25, quantity: '', class: "currency twenty-five", image: require("../assets/Moneda25.png")},
        ],
        currencyChange: [],
        usedChange: [
          {value: 1000, quantity: 0, type: "billete"},
          {value: 500, quantity: 0, type: "moneda"},
          {value: 100, quantity: 0, type: "moneda"},
          {value: 50, quantity: 0, type: "moneda"},
          {value: 25, quantity: 0, type: "moneda"}
        ],
        isCoffeeSelected: false,
        isBuyClicked: false,
        errorWhenBuying: false,
        requestedMoneyForChange: 0
      }
    },
    methods: {
      getCoffees() {
        axios.get("https://localhost:7148/api/Coffee").then((response) => {
          for (let i = 0; i < response.data.length; i++) {
            let coffeeName = response.data[i].name
            let coffeeQuantity = response.data[i].stock
            let coffeePrice = response.data[i].price
            this.coffees.push({name: coffeeName, quantity: coffeeQuantity, price: coffeePrice})
          }
        })
      },
      getCurrencyChange() {
        axios.get("https://localhost:7148/api/Change").then((response) => {
          for (let i = 0; i < response.data.length; i++) {
            console.log(response.data[i])
            let value = response.data[i].money
            let quantity = response.data[i].quantity
            let type = response.data[i].type
            this.currencyChange.push({value: value, quantity: quantity, type: type})
          }
        })
      },
      formatMoney(moneyString) {
        if (typeof(moneyString) === "number") moneyString = moneyString.toString()
        if (isNaN(Number(moneyString))) return "Inválido"
        console.log(moneyString)
        return "₡" + moneyString.replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1.");
      },
      validateCoffeeSelection(index) {
        if (this.coffeeBeforeSelection[index] === '' || 
           (this.coffeeSelected[index] != 0 && Number(this.coffeeBeforeSelection[index]) == 0)) return true
        return Number(this.coffeeBeforeSelection[index]) <= this.coffees[index].quantity && Number(this.coffeeBeforeSelection[index]) > 0;
      },
      setCoffeeSelection(index) {
        if (this.coffeeBeforeSelection[index] === '') return
        if (this.coffeeSelected[index] != 0 && Number(this.coffeeBeforeSelection[index]) == 0) this.coffeeSelected[index] = 0
        if (Number(this.coffeeBeforeSelection[index]) <= this.coffees[index].quantity && Number(this.coffeeBeforeSelection[index]) > 0) {
          this.coffeeSelected[index] = Number(this.coffeeBeforeSelection[index]);
          this.isCoffeeSelected = true
        }
      },
      getTotalCoffeeValue() {
        let totalValue = 0
        for (let i = 0; i < this.coffees.length; i++) {
          totalValue += this.coffeeSelected[i] * this.coffees[i].price
        }
        return totalValue
      },
      getIndividualCurrency(index) {
        let individualValue = this.currencies[index].value * this.currencies[index].quantity
        if (individualValue < 0) return "NaN"
        return individualValue.toString()
      },
      getRequestedMoney() {
        let totalValue = this.getTotalCoffeeValue()
        for (let i = 0; i < this.currencies.length; i++) {
          totalValue -= this.currencies[i].value * this.currencies[i].quantity
        }
        return totalValue
      },
      buyCoffee() {
        this.errorWhenBuying = false
        this.isBuyClicked = true
        let requestedMoney = this.getRequestedMoney()
        if (requestedMoney > 0 || !this.isCoffeeSelected) {
          return
        }
        this.requestedMoneyForChange = -requestedMoney

        let updatedChange = this.currencies.map(currency => parseInt(currency.quantity) || 0);

        axios.post("https://localhost:7148/api/Change", {
          requestedMoney: this.requestedMoneyForChange,
          updatedChange: updatedChange
        }).then((response) => {
          for (let i = 0; i < response.data.length; i++) {
            console.log(response.data[i])
            let value = response.data[i].money
            let quantity = response.data[i].quantity
            let type = response.data[i].type
            this.usedChange.push({value: value, quantity: quantity, type: type})
          }

          this.setNewCoffeeStock()
          for (let i = 0; i < this.coffeeSelected.length; i++) {
            this.coffeeSelected[i] = 0
            this.coffeeBeforeSelection[i] = ''
          }
          for (let i = 0; i < this.currencies.length; i++) {
            this.currencies[i].quantity = ''
          }
          this.isCoffeeSelected = false
        }).catch((error) => {
          console.log(error);
          this.errorWhenBuying = true;
        });
      },
      setNewCoffeeStock() {
        axios.post("https://localhost:7148/api/Coffee", this.coffeeSelected)
        .then((response) => {
          console.log(response)
          this.coffees = []
          this.currencyChange = []
          this.getCoffees()
          this.getCurrencyChange()
        }).catch((error) => {
          console.log(error);
          this.errorWhenBuying = true;
        });
      },
      getCurrencyChangeTotal(currency) {
        if (currency.quantity == 1) {
          return "    - 1 " + currency.type + " de " + this.formatMoney(currency.value)
        }
        return "    - " + currency.quantity.toString() + " " + currency.type + "s de " + this.formatMoney(currency.value)
      }
    },
    created() {
      this.getCoffees()
      this.getCurrencyChange()
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
  display: flex;
  flex-direction: column;
}

.coffee {
  background-color: white;
  margin: 10px 5px;
  display: flex;
  flex-direction: row;
  width: 100%;
  height: 100%;
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

.coffee v-text-field {
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
  align-self: start;
  transition: 0.2s ease all;
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
  width: 500px;
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
  margin-top: 15px;
  align-self: start;
}

.currency-img {
  width: 121px;
  display: flex;
  align-items: start;
  justify-content: center;
  margin-bottom: 10px;
}

hr {
  border: 1px solid rgb(209, 209, 209);
  border-radius: 5px;
  width: 100%;
}

.currency-required-amount {
  color: #ff0000;
  text-align: right;
  font-feature-settings: 'liga' off, 'clig' off;
  font-size: 24px;
  font-style: normal;
  font-weight: 500;
  line-height: 130%;
}

.currency-confirmation {
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
  transition: 0.2s ease all;
}

.useful-btn:hover {
  background-color: #a15a1c;
}

.coffee .disabled-btn, .checkout .disabled-btn {
  color: #9B9B9B;
  background-color: #D9D9D9;
  cursor: default;
}

</style>