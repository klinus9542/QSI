function [ ret ] = Termination( matrix )
    [terminationAbility, insteadMatrix] = termination(matrix);
    ret = {terminationAbility, insteadMatrix};
end

