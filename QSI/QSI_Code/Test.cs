using QuantumRuntime;
using QuantumRuntime.Operator;
using static QuantumRuntime.ControlStatement;
using static QuantumRuntime.Operator.E;
using static QuantumRuntime.Operator.M;
using static QuantumRuntime.Operator.U;
using static QuantumRuntime.Quantum;
using static QuantumRuntime.Registers;

namespace QSI.QSI_Code
{
    class Test : QEnv
    {
        public Reg r1 = new Reg("r1");
        public Reg r2 = new Reg("r2");
        public Reg r3 = new Reg("r3");
        public Reg r4 = new Reg("r4");
        public Reg r5 = new Reg("r5");
        public Reg r6 = new Reg("r6");
        public Reg r7 = new Reg("r7");
        public Reg r8 = new Reg("r8");
        public Reg r9 = new Reg("r9");
        public Reg r0 = new Reg("r0");


        //public QReg qOutput = new QReg();
        public Quantum LooperQ = MakeDensityOperator("{[1 0;0 0]}");
        //public Quantum q1 = MakeQBit("{[1/sqrt(2); 1/sqrt(2)]}");
        public Quantum q1 = MakeQBit("{[1;0]}");
        public Quantum q2 = MakeQBit("{[1;0]}");       
        public Quantum q3 = MakeQBit("{[1;0]}");
        public Quantum q4 = MakeQBit("{[1;0]}");
        public Quantum q5 = MakeQBit("{[1;0]}");
        public Quantum q6 = MakeQBit("{[1;0]}");
        public Quantum q7 = MakeQBit("{[1;0]}");
        public Quantum q8 = MakeQBit("{[1;0]}");
        public Quantum q9 = MakeQBit("{[1;0]}");
        public Quantum q0 = MakeQBit("{[1;0]}");

        U.Emit hGate = MakeU("{[1/sqrt(2) 1/sqrt(2); 1 / sqrt(2)  -1 / sqrt(2)]}");
        U.Emit CNot = MakeU("{[1 0 0 0;0 1 0 0;0 0 0 1;0 0 1 0]}");//1->2 Cnot
        U.Emit xGate = MakeU("{[0 1; 1 0]}");

        U.Emit zGate = MakeU("{[1 0;0 -1]}");

        M.Emit m = MakeM("{[1 0;0 0],[0 0;0 1]}");

        protected override void run()
        {

            xGate(q1);
            hGate(q2);
            hGate(q3);
            hGate(q4);
            hGate(q5);
            hGate(q6);
            hGate(q7);
            hGate(q8);
            hGate(q9);
            hGate(q0);

            Register(r1, m(q1));
            Register(r2, m(q2));
            Register(r3, m(q3));
            Register(r4, m(q4));
            Register(r5, m(q5));
            Register(r6, m(q6));
            Register(r7, m(q7));
            Register(r8, m(q8));
            Register(r9, m(q9));
            Register(r0, m(q0));
        }
    }
}
