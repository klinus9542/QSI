using QuantumRuntime;
using QuantumRuntime.Operator;
using static QuantumRuntime.ControlStatement;
using static QuantumRuntime.Operator.E;
using static QuantumRuntime.Operator.M;
using static QuantumRuntime.Operator.U;
using static QuantumRuntime.Quantum;
using static QuantumRuntime.Registers;

namespace UnitTest
{
    class TestQuantumConv0 : QEnv
    {

        public QReg qOutput = new QReg();
        public Quantum q1 = MakeDensityOperator("{[0.5 0.5;0.5 0.5]}");
        U.Emit hGate = MakeU("{[1/sqrt(2) 1/sqrt(2); 1 / sqrt(2)  -1 / sqrt(2)]}");
        U.Emit xGate = MakeU("{[0 1; 1 0]}");
        M.Emit m = MakeM("{[1 0;0 0],[0 0;0 1]}");
        public int CounterWhile = 0;
        protected override void run()//Only Run part needs generatation QASM  and draw circuits
        {


            QWhile(m(q1),
                () =>
                {
                    xGate(q1);
                    CounterWhile++;
                }
                );

            hGate(q1);

            QRegister(qOutput, q1);
        }
    }

    class TestQuantumConv1 : QEnv
    {
        public QReg qOutput = new QReg();
        public Quantum q1 = MakeDensityOperator("{[0.5 0.5;0.5 0.5]}");//|+><+| state
        U.Emit hGate = MakeU("{[1/sqrt(2) 1/sqrt(2); 1 / sqrt(2)  -1 / sqrt(2)]}");
        U.Emit xGate = MakeU("{[0 1; 1 0]}");
        M.Emit m = MakeM("{[1 0;0 0],[0 0;0 1]}");
        public int CounterWhile = 0;

        protected override void run()
        {
            QWhile(m(q1),
                () =>
                {
                    hGate(q1);
                    //hGate(q1);
                    CounterWhile++;
                }
                );
            hGate(q1);

            QRegister(qOutput, q1);
        }
    }


    class TestQuantumConv2 : QEnv
    {
        public Reg r1 = new Reg("r1");
        public QReg qOutput = new QReg();
        public Quantum q1 = MakeDensityOperator("{[0.5 0.5;0.5 0.5]}");//|+><+| state
        U.Emit hGate = MakeU("{[1/sqrt(2) 1/sqrt(2); 1 / sqrt(2)  -1 / sqrt(2)]}");
        U.Emit xGate = MakeU("{[0 1; 1 0]}");
        U.Emit zGate = MakeU("{[1 0;0 -1]}");
        E.Emit e = MakeE("{[1 0;0 0],[0 0;0 1]}");
        M.Emit m = MakeM("{[1 0;0 0],[0 0;0 1]}");

        protected override void run()//Only Run part needs generatation QASM  and draw circuits
        {
            hGate(q1);

            QIf(m(q1),
                () =>
                {
                    QWhile(m(q1),
                        () =>
                        {
                            hGate(q1);
                        }
                        );
                    //xGate(q1);
                },
                () =>
                {
                    xGate(q1);
                }
                );


            Register(r1, m(q1));
            QRegister(qOutput, q1);
        }
    }

    class TestQuantumConv3 : QEnv
    {
        public Reg r1 = new Reg("r1");
        public QReg qOutput = new QReg();
        public Quantum q1 = MakeDensityOperator("{[0.5 0.5;0.5 0.5]}");//|+><+| state
        U.Emit hGate = MakeU("{[1/sqrt(2) 1/sqrt(2); 1 / sqrt(2)  -1 / sqrt(2)]}");
        U.Emit xGate = MakeU("{[0 1; 1 0]}");
        U.Emit zGate = MakeU("{[1 0;0 -1]}");
        E.Emit e = MakeE("{[1 0;0 0],[0 0;0 1]}");
        M.Emit m = MakeM("{[1 0;0 0],[0 0;0 1]}");

        protected override void run()//Only Run part needs generatation QASM  and draw circuits
        {
            hGate(q1);

            QIf(m(q1),
                () =>
                {
                    //xGate(q1);
                },
                () =>
                {
                    xGate(q1);
                }
                );


            Register(r1, m(q1));
            QRegister(qOutput, q1);
        }
    }

    class TestQuantumConv4 : QEnv
    {
        public Reg r1 = new Reg("r1");
        public QReg qOutput = new QReg();
        public Quantum q1 = MakeDensityOperator("{[0.5 0.5;0.5 0.5]}");//|+><+| state
        U.Emit hGate = MakeU("{[1/sqrt(2) 1/sqrt(2); 1 / sqrt(2)  -1 / sqrt(2)]}");
        U.Emit xGate = MakeU("{[0 1; 1 0]}");
        U.Emit yGate = MakeU("{[0 1i;-1i 0]}");
        U.Emit zGate = MakeU("{[1 0;0 -1]}");
        E.Emit e = MakeE("{[1 0;0 0],[0 0;0 1]}");
        M.Emit m = MakeM("{[1 0;0 0],[0 0;0 1]}");

        protected override void run()//Only Run part needs generatation QASM  and draw circuits
        {
            hGate(q1);


            QWhile(m(q1),
            () =>
             {
                 QIf(m(q1),
                   () =>
                     {
                         yGate(q1);
                     },
                   () =>
                     {
                         xGate(q1);
                     });
             }
             );
            xGate(q1);

            Register(r1, m(q1));
            QRegister(qOutput, q1);
        }
    }


    class TestQuantumConv5 : QEnv
    {
        public Reg r1 = new Reg("r1");
        public QReg qOutput = new QReg();
        public Quantum q1 = MakeDensityOperator("{[0.5 0.5;0.5 0.5]}");//|+><+| state
        U.Emit hGate = MakeU("{[1/sqrt(2) 1/sqrt(2); 1 / sqrt(2)  -1 / sqrt(2)]}");
        U.Emit xGate = MakeU("{[0 1; 1 0]}");
        U.Emit yGate = MakeU("{[0 1i;-1i 0]}");
        U.Emit zGate = MakeU("{[1 0;0 -1]}");
        E.Emit e = MakeE("{[1 0;0 0],[0 0;0 1]}");
        M.Emit m = MakeM("{[1 0;0 0],[0 0;0 1]}");

        protected override void run()//Only Run part needs generatation QASM  and draw circuits
        {
            hGate(q1);


            QWhile(m(q1),
            () =>
            {
                QIf(m(q1),
                  () =>
                  {
                      QWhile(m(q1),
                          () =>
                          {
                              xGate(q1);
                          });
                  },
                  () =>
                  {
                      xGate(q1);
                  });
            }
             );
            xGate(q1);

            QWhile(m(q1),
           () =>
           {
               QIf(m(q1),
                  () =>
                  {
                      yGate(q1);
                  },
                  () =>
                  {
                      xGate(q1);
                  });
           });

            Register(r1, m(q1));
            QRegister(qOutput, q1);
        }
    }

    class TestQuantumConv6 : QEnv
    {
        public Reg r1 = new Reg("r1");
        public QReg qOutput = new QReg();
        public Quantum q1 = MakeDensityOperator("{[0.5 0.5;0.5 0.5]}");
        public Quantum q2 = MakeDensityOperator("{[0.5 0.5;0.5 0.5]}");
        U.Emit hGate = MakeU("{[1/sqrt(2) 1/sqrt(2); 1 / sqrt(2)  -1 / sqrt(2)]}");
        U.Emit u1Gate = MakeU("{[1 0 ; 0 exp(-1i*pi/16)]}");
        U.Emit zGate = MakeU("{[1 0;0 -1]}");
        U.Emit xGate = MakeU("{[0 1; 1 0]}");
        E.Emit e = MakeE("{[1 0;0 0],[0 0;0 1]}");
        M.Emit m = MakeM("{[1 0;0 0],[0 0;0 1]}");

        protected override void run()//Only Run part needs generatation QASM  and draw circuits
        {
            hGate(q1);

            QWhile(m(q2),
                () =>
                {
                    hGate(q2);
                }
                );

            Register(r1, m(q1));
            QRegister(qOutput, q1);
        }
    }

}
