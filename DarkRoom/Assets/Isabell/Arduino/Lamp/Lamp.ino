int relay = 23;
bool isCommandRead = false;

void setup() {
  pinMode(relay, OUTPUT);
  Serial.begin(115200);

  // wait for serial port to connect. Needed for native USB
  while (!Serial) 
  {}
}

void loop() {
  /*digitalWrite(relay, HIGH);
  delay(1000);
  digitalWrite(relay, LOW);
  delay(1000);*/
  ProcessInput();
  if(isCommandRead){
    DoCommand();
  }
}

String command;
void DoCommand()
{
  command.trim();
  command.toLowerCase();

  if(command == F("turnonled"))
  {
    digitalWrite(relay, HIGH);
  }
  else if(command == F("turnoffled"))
  {
    digitalWrite(relay, LOW);
  }
}

void ProcessInput()
{
  isCommandRead = false;
  command = F("");

  if(Serial.available() > 0)
    isCommandRead = true;

  while(Serial.available() > 0)
  {
    command = Serial.readString();

#ifdef DEBUG
    Serial.print(F("//"));
    Serial.println(command);
#endif
  }
}
