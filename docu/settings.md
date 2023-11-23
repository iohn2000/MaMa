# Settings File to Define Calculation

A settings file can have 1 or more different definitions for calculations. 
The definition defines certain properties like size but the actual number is randomly generated.

A definition has name and contains 4 sections:
 1. firstNumber
 1. secondNumber
 1. solutionCriteria
 1. amount

## First-, Second Number
firstNumber and secondNumber have the same properties and represent the 2 numbers the calculation is made of.
e.g. `4 x 6 =` or `12 : 4 =` 
It is defined as follows :
 - Division: 
   - firstNumber : Dividend
   - secondNumber : Divisor
 - Multiplication: 
   - firstNumber : factor 1, multiplicand
   - secondNumber : factor 2, multiplier
 - Subtraction: 
   - firstNumber : Minuend
   - secondNumber : Subtrahend

### Properties First-, Second Number

#### maxDigits, minValue, maxValue
Here it is defined how big the number is.
`minValue` and `maxValue` have priority, if they are not set then `maxDigits` property is used.
Examples: 
  - minValue=0, maxValue=10 -> the random number will be between 0 and 10
  - minValue=0, maxValue=20, maxDigits=10 -> random nr between 0 and 20, maxDigits is ignored
  - minValue=, maxValue=, maxDigits=4 -> random nr between 0 and 9999 is generated

#### moveKomma, allowNegativ
Once the number is generated some modification can be applied.

- `allowNegative`: a randomizer turns the number negative or not
- `moveKomma`: **maximum** amount of decimal places a comma is moved, the actual number is a random number between 0 and the value of `moveKomma`,  
  starting at ones column (Einerstelle or leftmost decimal place)  
  Examples: 
    - if value is `2` and the random number is `4128` the result is `41,28` 
    - value is `5` and number `12` result is `0,00012`

   
## Basic Arithmetical Operations

``` json
{
  "BasicArithmeticalOperation": {
    "multi": {
      "firstNumber": {
        "maxDigits": 5,
        "moveKomma": 0,
        "maxValue": 99999,
        "minValue": 1000,
        "allowNegative": false
      },
      "secondNumber": {
        "maxDigits": 5,
        "moveKomma": 0,
        "maxValue": 99,
        "minValue": 50,
        "allowNegative": false
      },
      "solutionCriteria": {
        "numberClass": "Integer",
        "digitsAfterCommaRange": "0",
        "allowNegative": false,
        "elementaryArithmetic": "Multiplikation"
      },
      "amount": 3
    },
    "2-stellig-Div-KeinRest": {
      "firstNumber": {
        "maxDigits": 5,
        "moveKomma": 0,
        "maxValue": 99999,
        "minValue": 500,
        "allowNegative": false
      },
      "secondNumber": {
        "maxDigits": 2,
        "moveKomma": 0,
        "maxValue": 99,
        "minValue": 12,
        "allowNegative": false
      },
      "solutionCriteria": {
        "numberClass": "Integer",
        "digitsAfterCommaRange": "0",
        "allowNegative": false,
        "elementaryArithmetic": "Division"
      },
      "amount": 3
    },
    "2-stellig-Div-MitRest": {
      "firstNumber": {
        "maxDigits": 5,
        "moveKomma": 0,
        "maxValue": 99999,
        "minValue": 500,
        "allowNegative": false
      },
      "secondNumber": {
        "maxDigits": 2,
        "moveKomma": 0,
        "maxValue": 99,
        "minValue": 12,
        "allowNegative": false
      },
      "solutionCriteria": {
        "numberClass": "Any",
        "digitsAfterCommaRange": "0",
        "allowNegative": false,
        "elementaryArithmetic": "Division"
      },
      "amount": 3
    }
  },
  "Fractions": null
}
```

