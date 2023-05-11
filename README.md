"# MaMa" 
Generate Random math problems for primary schools (Volksschule).
Type of calculation (plus, minus, multplication, division) and many paramteres concernen the random numbers and type of result can be configured in a json file.
Example below produces :
- 3 multiplications with first number between 1000 and 99999 with no comma (moveComma)
- 3 division where the result has no comma (no remainder) "Integer"
- 3 division with any result (including periodic non-periodic rational numbers)


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


PdfSharpt Samples : http://www.pdfsharp.net/wiki/MigraDocSamples.ashx
