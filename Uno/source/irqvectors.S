.extern main
.extern stdhandler

.global IRQTable
IRQTable:
;Einstiegspunkt und Interrupt
rjmp main
rjmp stdhandler ;ExInt 0
rjmp stdhandler ;ExInt 1
rjmp stdhandler ;PinChange 0
rjmp stdhandler ;PinChange 1
rjmp stdhandler ;PinChange 2
rjmp stdhandler ;WDT
rjmp stdhandler ;Timer2 CompA
rjmp stdhandler ;Timer2 CompB
rjmp stdhandler ;Timer2 OVF
rjmp stdhandler ;Timer1 CAPT
rjmp stdhandler ;Timer1 CompA
rjmp stdhandler ;Timer1 CompB
rjmp stdhandler ;Timer1 OVF
rjmp stdhandler ;Timer0 CompA
rjmp stdhandler ;Timer0 CompB
rjmp stdhandler ;Timer0 OVF
rjmp stdhandler ;SPI STC
rjmp stdhandler ;USART, RX Complete
rjmp stdhandler ;USART, Data Register Empty
rjmp stdhandler ;USART, TX Complete
rjmp stdhandler ;ADC Conversion Complete
rjmp stdhandler ;EEPROM Ready
rjmp stdhandler ;AnalogComperator
rjmp stdhandler ;2-wire Interface
rjmp stdhandler ;Store Program Memory Ready
