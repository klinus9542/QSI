from skc.operator import *
from skc.dawson.factor import *
from skc.dawson import *
from skc.compose import *
from skc.basis import *
from skc.simplify import *
import skc.utils
import math

#
import scipy.io
#

H2 = get_hermitian_basis(d=2)
# theta = math.pi / 4  # 45 degrees, already division 2
# axis = cart3d_to_h2(x=1, y=1, z=1)

#theta = math.pi / 4
#axis = cart3d_to_h2(x=0, y=1, z=0)

print "Identity Name: " + H2.identity.name

# Compose a unitary to compile
#matrix_U = axis_to_unitary(axis, theta, H2)

#matrix_U = matrixify([[1, 0], [0, -1]]) #test case 1,Z gate
#matrix_U = matrixify([[0, -1j], [1j, 0]]) #test case 2, Y gate

#
matrix_U = scipy.io.loadmat(sys.argv[1])['matrix']
#
op_U = Operator(name="U", matrix=matrix_U)

#
n = 1
#

print "U= " + str(matrix_U)

##############################################################################
# Prepare the simplify engine
# Simplifying rules
identity_rule = IdentityRule(id_sym=H2.identity.name)
double_H_rule = DoubleIdentityRule('H', id_sym=H2.identity.name)
double_I_rule = DoubleIdentityRule(H2.identity.name, id_sym=H2.identity.name)
# H_non_dagger_rule = NonDaggerRule('H')
# I_non_dagger_rule = NonDaggerRule(H2.identity.name)
adjoint_rule = AdjointRule()
T8_rule = GeneralRule(['T', 'T', 'T', 'T', 'T', 'T', 'T', 'T'], H2.identity.name)
Td8_rule = GeneralRule(['Td', 'Td', 'Td', 'Td', 'Td', 'Td', 'Td', 'Td'], H2.identity.name)
# We should also add a rule for 8T gates -> I

simplify_rules = [
    identity_rule,
    double_H_rule,
    double_I_rule,
    adjoint_rule,
    T8_rule,
    Td8_rule
]

simplify_engine = SimplifyEngine(simplify_rules)

skc.utils.self_adjoint_operators = ['H', H2.identity.name]

# Prepare the compiler
sk_set_factor_method(dawson_group_factor)
sk_set_basis(H2)
sk_set_axis(X_AXIS)
sk_set_simplify_engine(simplify_engine)
sk_build_tree("su2", 15)

Un = solovay_kitaev(op_U, n)
print "Approximated U: " + str(Un)

print "Un= " + str(Un.matrix)
print "op_U=" + str(op_U.matrix)

print "trace_dist(U,Un)= " + str(trace_distance(Un.matrix, op_U.matrix))
print "fowler_dist(U,Un)= " + str(fowler_distance(Un.matrix, op_U.matrix))
print "sequence length= " + str(len(Un.ancestors))
print "n= " + str(n)
