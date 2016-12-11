%% Product matrix representation
% This function can be called as a way:
%
%   requires: nothing
%   author: Shusen Liu(klinus@outlook.com)
%   package: termination
%   last updated: July 4, 2016

function matrix_representation= representation(A,varargin)

if(iscell(A))
    matrix_representation = kron(A{1},conj(A{1}));
    for j = 2:length(A);
        temp=kron(A{j},conj(A{j}));
        matrix_representation=matrix_representation+temp;
    end
elseif(nargin == 1)
    matrix_representation = kron(A,conj(A));
else
    matrix_representation=kron(A,conj(A));
    for j = 2:nargin;
        temp=kron(varargin{j-1},conj(varargin{j-1}));
        matrix_representation=matrix_representation+temp;
    end
end