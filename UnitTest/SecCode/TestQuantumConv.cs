using QuantumRuntime;
using QuantumRuntime.Operator;
using static QuantumRuntime.ControlStatement;
using static QuantumRuntime.Operator.E;
using static QuantumRuntime.Operator.M;
using static QuantumRuntime.Operator.U;
using static QuantumRuntime.Quantum;

namespace UnitTest
{
    class TestQuantumConv0 : QLangBase
    {
        public TestQuantumConv0()
        {
            InitAllValues();
        }

        public Reg r1 = new Reg("r1");
        public QReg qOutput = new QReg();
        public Quantum q1 = MakePureDensityOperator(2, "{[0.5 0.5;0.5 0.5]}");
        U.Emit hGate = MakeU("{[1/sqrt(2) 1/sqrt(2); 1 / sqrt(2)  -1 / sqrt(2)]}");
        U.Emit xGate = MakeU("{[0 1; 1 0]}");
        U.Emit zGate = MakeU("{[1 0;0 -1]}");
        U.Emit zeroGate = MakeU("{[1 0; 0 0]}");
        E.Emit e = MakeE("{[1 0;0 0],[0 0;0 1]}");
        M.Emit m = MakeM("{[1 0;0 0],[0 0;0 1]}");

        public void Run()//Only Run part needs generatation QASM  and draw circuits
        {
            xGate(q1);

            QWhile(m(q1),
                () =>
                {
                    hGate(q1);
                }
                );
            zeroGate(q1);

            Register(r1, m(q1));
            QRegister(qOutput, q1);
        }
    }

    class TestQuantumConv1 : QLangBase
    {
        public TestQuantumConv1()
        {
            InitAllValues();
        }

        public Reg r1 = new Reg("r1");
        public QReg qOutput = new QReg();
        public Quantum q1 = MakePureDensityOperator(2, "{[0.5 0.5;0.5 0.5]}");//|+><+| state
        U.Emit hGate = MakeU("{[1/sqrt(2) 1/sqrt(2); 1 / sqrt(2)  -1 / sqrt(2)]}");
        U.Emit xGate = MakeU("{[0 1; 1 0]}");
        U.Emit zGate = MakeU("{[1 0;0 -1]}");
        U.Emit zeroGate = MakeU("{[1 0; 0 0]}");
        E.Emit e = MakeE("{[1 0;0 0],[0 0;0 1]}");
        M.Emit m = MakeM("{[1 0;0 0],[0 0;0 1]}");

        public void Run()//Only Run part needs generatation QASM  and draw circuits
        {
            hGate(q1);

            QWhile(m(q1),
                () =>
                {
                    hGate(q1);
                }
                );
            e(q1);

            Register(r1, m(q1));
            QRegister(qOutput, q1);
        }
    }


    class TestQuantumConv2 : QLangBase
    {
        public TestQuantumConv2()
        {
            InitAllValues();
        }

        public Reg r1 = new Reg("r1");
        public QReg qOutput = new QReg();
        public Quantum q1 = MakePureDensityOperator(2, "{[0.5 0.5;0.5 0.5]}");//|+><+| state
        U.Emit hGate = MakeU("{[1/sqrt(2) 1/sqrt(2); 1 / sqrt(2)  -1 / sqrt(2)]}");
        U.Emit xGate = MakeU("{[0 1; 1 0]}");
        U.Emit zGate = MakeU("{[1 0;0 -1]}");
        U.Emit zeroGate = MakeU("{[1 0; 0 0]}");
        E.Emit e = MakeE("{[1 0;0 0],[0 0;0 1]}");
        M.Emit m = MakeM("{[1 0;0 0],[0 0;0 1]}");

        public void Run()//Only Run part needs generatation QASM  and draw circuits
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

    class TestQuantumConv3 : QLangBase
    {
        public TestQuantumConv3()
        {
            InitAllValues();
        }

        public Reg r1 = new Reg("r1");
        public QReg qOutput = new QReg();
        public Quantum q1 = MakePureDensityOperator(2, "{[0.5 0.5;0.5 0.5]}");//|+><+| state
        U.Emit hGate = MakeU("{[1/sqrt(2) 1/sqrt(2); 1 / sqrt(2)  -1 / sqrt(2)]}");
        U.Emit xGate = MakeU("{[0 1; 1 0]}");
        U.Emit zGate = MakeU("{[1 0;0 -1]}");
        U.Emit zeroGate = MakeU("{[1 0; 0 0]}");
        E.Emit e = MakeE("{[1 0;0 0],[0 0;0 1]}");
        M.Emit m = MakeM("{[1 0;0 0],[0 0;0 1]}");

        public void Run()//Only Run part needs generatation QASM  and draw circuits
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

    class TestQuantumConv4 : QLangBase
    {
        public TestQuantumConv4()
        {
            InitAllValues();
        }

        public Reg r1 = new Reg("r1");
        public QReg qOutput = new QReg();
        public Quantum q1 = MakePureDensityOperator(2, "{[0.5 0.5;0.5 0.5]}");//|+><+| state
        U.Emit hGate = MakeU("{[1/sqrt(2) 1/sqrt(2); 1 / sqrt(2)  -1 / sqrt(2)]}");
        U.Emit xGate = MakeU("{[0 1; 1 0]}");
        U.Emit zGate = MakeU("{[1 0;0 -1]}");
        U.Emit zeroGate = MakeU("{[1 0; 0 0]}");
        E.Emit e = MakeE("{[1 0;0 0],[0 0;0 1]}");
        M.Emit m = MakeM("{[1 0;0 0],[0 0;0 1]}");

        public void Run()//Only Run part needs generatation QASM  and draw circuits
        {
            hGate(q1);

           
            QWhile(m(q1),
            () =>
             {
                 QIf(m(q1),
                   () =>
                     { },
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

 }
