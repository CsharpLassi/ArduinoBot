char version[] = "0.1";
char name[] = "Test-Arduino";

void setup() {
  Serial.begin(9600);

  while(true)
  {
     delay(100);
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

void loop() {
  // put your main code here, to run repeatedly:

}
