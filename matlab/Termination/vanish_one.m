%% Vanish the jordan blocks correspongding to those eigenvalues with modules 1
% which are all 1-dimensional according to Lemma 4.1
% This function can be called as a way:
%
%   requires: nothing
%   author: Shusen Liu(klinus@outlook.com)
%   package: termination
%   last updated: July 5, 2016
function new_representation=vanish_one(matrix_representation)
% get diag
diag_matrix_representation=diag(matrix_representation);

%get norm of every item
abs_diag_matrix_representation=abs(diag_matrix_representation);

%get every modules 1 index
 idx=find(abs_diag_matrix_representation==1);
 
%modify them into zero
new_representation=matrix_representation;
    
new_representation(sub2ind(size(new_representation), idx, idx))=0;
end
