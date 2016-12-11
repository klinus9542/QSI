%version 2
%Termination output instead_matrix is modified, considering adding 

%termination_ability=2;Termination
%termination_ability=1;Almost Termination
%termination_ability=0;Non Termination

function [termination_ability,instead_matrix]=termination(matrix_representation)


%Find Jordan decomposition and
% decend order eignvalue and eigenvector
[V,J]=decend_jordan(matrix_representation);

%find maxEnageledstate
dim_maxEntangled=sqrt(size(matrix_representation,1));
%test_case dim_maxEntangled=2;
maxEntangleState=MaxEntangled(dim_maxEntangled,0,1);

%recomposition V^-1 * maxEntangledState
%aid_vector=S^-1 |psi>
aid_vector=(V^-1 )* maxEntangleState
assert(isvector(aid_vector));

fore_0_aid_vector=find(aid_vector,1)-1

%get the diag of V
diag_j=diag(J);

%Begin to check wether it is termination
%find eigenvalue which great than 0
num_of_ge_0 = length(find(diag_j));

if(fore_0_aid_vector>=num_of_ge_0);
    termination_ability=2;
    %cal instead Matrix representation
    instead_matrix=zeros(size(matrix_representation));
    for count=1:size(matrix_representation,1);
        temp_count_matrix=matrix_representation^count;
        instead_matrix=instead_matrix+temp_count_matrix;
        if(all((temp_count_matrix)*aid_vector)==0)
           break;
        end
    end
    
    return
end 
%END check wether it is termination


%Begin to check wether it is almost termination
%find eigenvalue whose norm equals 1
norm_diag_v= abs(diag_j);
num_of_eq_1=length(find(norm_diag_v==1));

if(fore_0_aid_vector>=num_of_eq_1);
    termination_ability=1;
    new_representation=vanish_one(matrix_representation);
    instead_matrix=inv(eye(size(new_representation))-new_representation);
    return
end
%ENd check Almost termination

%Begin to output Non-termination
termination_ability=0;
instead_matrix=0
return