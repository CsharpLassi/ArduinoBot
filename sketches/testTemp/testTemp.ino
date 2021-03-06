// Internal Temperature Sensor
// Example sketch for ATmega328 types.
//
// April 2012, Arduino 1.0

const double QUANTILE = 2.262;

void setup()
{
  Serial.begin(9600);

  Serial.println(F("Internal Temperature Sensor"));
}

void loop()
{
  // Show the temperature in degrees Celsius.

  double values[10]; 
  double average = 0.0;
  double delta = 0.0;
  double stddelta = 0.0; 
  double range = 0.0;
  for(int i = 0; i < 10; i++)
  {
    double value = GetTemp();
    values[i] = value;
    average += value;
  }

  //Mittelwert
  average = average / 10;

  //Standarabweichung
  for(int i = 0; i < 10; i++)
  {
    double diff = (values[i]-average);
    delta += diff * diff ;
  }

  delta = sqrt(delta/9);
  stddelta = delta / sqrt(10);
  range = stddelta * QUANTILE;
  
  
  Serial.println(average ,1);
  Serial.println(range ,1);
  delay(1000);
}

double GetTemp(void)
{
  unsigned int wADC;
  double t;

  // The internal temperature has to be used
  // with the internal reference of 1.1V.
  // Channel 8 can not be selected with
  // the analogRead function yet.

  // Set the internal reference and mux.
  ADMUX = (_BV(REFS1) | _BV(REFS0) | _BV(MUX3));
  ADCSRA |= _BV(ADEN);  // enable the ADC

  delay(20);            // wait for voltages to become stable.

  ADCSRA |= _BV(ADSC);  // Start the ADC

  // Detect end-of-conversion
  while (bit_is_set(ADCSRA,ADSC));

  // Reading register "ADCW" takes care of how to read ADCL and ADCH.
  wADC = ADCW;
  // The offset of 324.31 could be wrong. It is just an indication.
  t = (wADC - 324.31 ) / 1.22;

  // The returned temperature is in degrees Celsius.
  return (t);
}
