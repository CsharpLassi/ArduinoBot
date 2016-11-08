.include "m328Pdef.S"
.include "register.S"

.global main
.global stdhandler

.extern Uart_Init
.extern print

.text
main:
	;Ram
	ldi temp , hi8(RAMEND)
	out SPH  , temp
	ldi temp , lo8(RAMEND)
	out SPL  , temp

	rcall Uart_Init

loop:
	ldi 30, lo8(txt_hello)
	ldi 31, hi8(txt_hello)
	rcall   print
	rjmp    loop

stdhandler:
	reti

.data
txt_hello: .asciz "Hello World\n\r"
