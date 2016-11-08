SECTIONS
{

    init :
    {
      build/irqvectors.o
      build/main.o
    }
    .text :
    {
      *(.text)
    }
    .data : { *(.data) }
    .bss : { *(.bss) }
}
