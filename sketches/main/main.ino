char version[] = "0.1";
char name[] = "Test-Arduino";

void setup() {
  Serial.begin(9600);

  while(true)
  {
     delay(1000);
     if(Serial.available() > 0)
     {
      char read = Serial.read();
      if(read == 's')
      {
        Serial.write('o');
        break;
      }
     }
     Serial.write('w');
  }
  PrintData(version,sizeof(version));
  PrintData(name,sizeof(name));
}

void PrintData(char* data,long size)
{
  Serial.write(size);
  Serial.write(data,size);
}

void PrintData(uint16_t data)
{
  Serial.write((char*)&data,2);
}

void PrintHeader(uint16_t id,uint16_t len)
{
  PrintData(id);
  PrintData(len);
}

void loop() 
{
  //Interne Temperaturmessung
  PrintHeader(10,10);
  for(byte i = 0; i<10;i++)
  {
    PrintData(GetTemp());
  }
}

uint16_t GetTemp(void)
{
  ADMUX = (_BV(REFS1) | _BV(REFS0) | _BV(MUX3));
  ADCSRA |= _BV(ADEN);  // enable the ADC

  delay(20);

  ADCSRA |= _BV(ADSC);

  while (bit_is_set(ADCSRA,ADSC));

  return ADCW;
}
