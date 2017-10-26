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

        //public QReg qOutput = new QReg();
        public Quantum LooperQ = MakeDensityOperator("{[1 0;0 0]}");
        public Quantum q1 = MakeQBit("{[1/sqrt(2); 1/sqrt(2)]}");
        public Quantum q2 = MakeDensityOperator("{[0.5 0.5;0.5 0.5]}");//1/2(|0>+|1>)
        public Quantum q3 = MakeDensityOperator("{[1 0;0 0]}");
        U.Emit hGate = MakeU("{[1/sqrt(2) 1/sqrt(2); 1 / sqrt(2)  -1 / sqrt(2)]}");
        U.Emit CNot = MakeU("{[1 0 0 0;0 1 0 0;0 0 0 1;0 0 1 0]}");//1->2 Cnot
        U.Emit xGate = MakeU("{[0 1; 1 0]}");
        U.Emit TesterGate= MakeU("{[-0.3019 -0.1336 -0.2361 -0.9139; -0.2286   -0.7497   -0.5309    0.3223; -0.8316   -0.0607    0.5324    0.1460; 0.4063   -0.6453    0.6156   -0.1989]}");
        U.Emit zGate = MakeU("{[1 0;0 -1]}");

        M.Emit m = MakeM("{[1 0;0 0],[0 0;0 1]}");

        protected override void run()
        {
            CNot(q1, q3);

            Register(r1, m(q1));
        }
    }
}
