.include "m328Pdef.S"
.include "register.S"

; Berechnungen
.equ UBRR_VAL   , ((F_CPU+BAUD*8)/(BAUD*16)-1)  ; clever runden
.equ BAUD_REAL  , (F_CPU/(16*(UBRR_VAL+1)))     ; Reale Baudrate
.equ BAUD_ERROR , ((BAUD_REAL*1000)/BAUD-1000)  ; Fehler in Promille

.if ((BAUD_ERROR>10) || (BAUD_ERROR<-10))       ; max. +/-10 Promille Fehler
  .error "Systematischer Fehler der Baudrate grösser 1 Prozent und damit zu hoch!"
.endif

.global Uart_Init
.global print

Uart_Init:
  ;Baudrate einstellen
  ldi     temp, hi8(UBRR_VAL)
  sts	  UBRR0H, temp
  ldi     temp, lo8(UBRR_VAL)
  sts     UBRR0L, temp

  ; Frame-Format: 8 Bit

  ldi     temp, (1<<UCSZ01)|(1<<UCSZ00)
  sts     UCSR0C, temp

  ldi	temp, (1 << TXEN0)
  sts     UCSR0B,temp                  ; TX aktivieren

  ret

print:
    lpm zeichen, Z+
    tst zeichen
    breq print_end
    rcall serout
    rjmp print
print_end:
    rcall sync
    ret

serout:
    lds	    temp, UCSR0A
    sbis    temp, UDRE0                 ; Warten bis UDR für das nächste
                                        ; Byte bereit ist
    rjmp    serout
    sts     UDR0, zeichen
    ret                                 ; zurück zum Hauptprogramm

; kleine Pause zum Synchronisieren des Empfängers, falls zwischenzeitlich
; das Kabel getrennt wurde

sync:
    ldi     16,0
sync_1:
    ldi     17,0
sync_loop:
    dec     17
    brne    sync_loop
    dec     16
    brne    sync_1
    ret
