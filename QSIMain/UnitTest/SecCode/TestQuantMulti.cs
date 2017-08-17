using QuantumRuntime;
using QuantumRuntime.Operator;
using static QuantumRuntime.ControlStatement;
using static QuantumRuntime.Operator.E;
using static QuantumRuntime.Operator.M;
using static QuantumRuntime.Operator.U;
using static QuantumRuntime.Quantum;
using static QuantumRuntime.Registers;
using System;

namespace UnitTest
{
    class TestQuantMulti0 : QEnv
    {
        public Reg r1 = new Reg("r1");
        public Reg r2 = new Reg("r2");
        public Quantum q1 = MakeDensityOperator("{[0.5 0.5;0.5 0.5]}");
        public Quantum q2 = MakeDensityOperator("{[1 0;0 0]}");
        U.Emit CNot = MakeU("{[1 0 0 0;0 1 0 0;0 0 0 1;0 0 1 0]}");//1->2 Cnot
        M.Emit m = MakeM("{[1 0;0 0],[0 0;0 1]}");

        protected override void run()
        {
            CNot(q1, q2);
            Register(r1, m(q1));
            Register(r2, m(q2));
        }

    }

    class TestQuantMulti1 : QEnv //Quantum Teleportation
    {
        public Reg r3 = new Reg("r3");
        public Quantum Alice = MakeQBit("{[1/sqrt(5); sqrt(4)/sqrt(5)]}");
        public Quantum Bob1 = MakeDensityOperator("{[0.5 0.5;0.5 0.5]}");//|0>+|1>
        public Quantum Bob2 = MakeDensityOperator("{[1 0;0 0]}");
        U.Emit hGate = MakeU("{[1/sqrt(2) 1/sqrt(2); 1 / sqrt(2)  -1 / sqrt(2)]}");
        U.Emit CNot = MakeU("{[1 0 0 0;0 1 0 0;0 0 0 1;0 0 1 0]}");//1->2 Cnot
        U.Emit xGate = MakeU("{[0 1; 1 0]}");
        U.Emit zGate = MakeU("{[1 0;0 -1]}");
        M.Emit m = MakeM("{[1 0;0 0],[0 0;0 1]}");
        protected override void run()
        {
            CNot(Bob1, Bob2); //Prepare |00>+|11> for Bob
            CNot(Alice, Bob1);
            hGate(Alice);
            QIf(m(Bob1),
                () =>
                { },
                () =>
                {
                    xGate(Bob2);
                });
            QIf(m(Alice),
              () =>
              { },
              () =>
              {
                  zGate(Bob2);
              });

            Register(r3, m(Bob2));
        }
    }

    class TestQuantMulti2 : QEnv //Quantum superdense coding
    {
        public Reg r1 = new Reg("r1");
        public Reg r2 = new Reg("r2");
        public Reg r3 = new Reg("r3");

        public QReg qOutput = new QReg();
        public Quantum Alice = MakeDensityOperator("{[0.5 0.5;0.5 0.5]}");
        public Quantum Bob = MakeDensityOperator("{[1 0;0 0]}");
        U.Emit hGate = MakeU("{[1/sqrt(2) 1/sqrt(2); 1 / sqrt(2)  -1 / sqrt(2)]}");
        U.Emit CNot = MakeU("{[1 0 0 0;0 1 0 0;0 0 0 1;0 0 1 0]}");//1->2 Cnot
        U.Emit xGate = MakeU("{[0 1; 1 0]}");
        U.Emit zGate = MakeU("{[1 0;0 -1]}");
        U.Emit IGate = MakeU("{[1 0;0 1]}");
        U.Emit iyGate = MakeU("{[0 1;-1 0]}");
        //E.Emit e = MakeE("{[1 0;0 0],[0 0;0 1]}");
        //M.Emit m = MakeM("{[1 0;0 0],[0 0;0 1]}");
        M.Emit BellMeasure = MakeM("{(1/2)*[1 0 0 1;0 0 0 0;0 0 0 0;1 0 0 1],(1/2)*[1 0 0 -1;0 0 0 0;0 0 0 0;-1 0 0 1],(1/2)*[0 0 0 0;0 1 1 0;0 1 1 0;0 0 0 0],(1/2)*[0 0 0 0 ;0 1 -1 0; 0 -1 1 0; 0 0 0 0]}");

        public int SWITCHTO = 2;

        protected override void run()//Only Run part needs generatation QASM  and draw circuits
        {
            CNot(Alice, Bob); //Prepare |00>+|11> for Bob

            switch (SWITCHTO)
            {
                case 1: //00:I
                    IGate(Alice);
                    break;
                case 2: //01:Z
                    zGate(Alice);
                    break;
                case 3:
                    xGate(Alice);
                    break;
                case 4:
                    iyGate(Alice);
                    break;
            }

            Register(r3, BellMeasure(Alice, Bob));

        }
    }

    class TestQuantMulti3 : QEnv //Quantum Teleportation with LOOPS
    {
        public Reg r1 = new Reg("r1");
        public Reg r2 = new Reg("r2");
        public Reg r3 = new Reg("r3");

        public QReg qOutput = new QReg();
        public Quantum LooperQ = MakeDensityOperator("{[1 0;0 0]}");
        public Quantum Alice = MakeQBit("{[1/sqrt(5); sqrt(4)/sqrt(5)]}");
        public Quantum Bob1 = MakeDensityOperator("{[0.5 0.5;0.5 0.5]}");//1/2(|0>+|1>)
        public Quantum Bob2 = MakeDensityOperator("{[1 0;0 0]}");
        U.Emit hGate = MakeU("{[1/sqrt(2) 1/sqrt(2); 1 / sqrt(2)  -1 / sqrt(2)]}");
        U.Emit CNot = MakeU("{[1 0 0 0;0 1 0 0;0 0 0 1;0 0 1 0]}");//1->2 Cnot
        U.Emit xGate = MakeU("{[0 1; 1 0]}");

        U.Emit zGate = MakeU("{[1 0;0 -1]}");

        M.Emit m = MakeM("{[1 0;0 0],[0 0;0 1]}");

        protected override void run()//Only Run part needs generatation QASM  and draw circuits
        {
            hGate(LooperQ);
            QWhile(m(LooperQ),
                () =>
                {
                    hGate(LooperQ); //For termination analysis


                    CNot(Bob1, Bob2); //Prepare |00>+|11> for Bob
                    CNot(Alice, Bob1);
                    hGate(Alice);
                    QIf(m(Bob1),
                        () =>
                        { },
                        () =>
                        {
                            xGate(Bob2);
                        });
                    QIf(m(Alice),
                      () =>
                      { },
                      () =>
                      {
                          zGate(Bob2);
                      });
                }
            );
            Register(r3, m(Bob2));

        }
    }
}
