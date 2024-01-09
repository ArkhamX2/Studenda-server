import 'package:dartz/dartz.dart';
import 'package:studenda_mobile/core/data/error/failure.dart';
import 'package:studenda_mobile/core/data/usecases/usecase.dart';
import 'package:studenda_mobile/feature/auth/data/models/security_request_model.dart';
import 'package:studenda_mobile/feature/auth/data/models/security_response_model.dart';
import 'package:studenda_mobile/feature/auth/domain/repositories/auth_repository.dart';

class Auth extends Usecase<SecurityResponseModel,SecurityRequestModel>{
  final AuthRepository authRepository;

  Auth({required this.authRepository});

  @override
  Future<Either<Failure,SecurityResponseModel>> call(SecurityRequestModel request) async{
    return await authRepository.auth(request);
  }
}
