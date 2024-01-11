import 'package:dartz/dartz.dart';
import 'package:studenda_mobile/core/data/error/exception.dart';
import 'package:studenda_mobile/core/data/error/failure.dart';
import 'package:studenda_mobile/core/network/network_info.dart';
import 'package:studenda_mobile/feature/auth/data/datasources/auth_remote_datasource.dart';
import 'package:studenda_mobile/feature/auth/data/models/security_request_model.dart';
import 'package:studenda_mobile/feature/auth/data/models/security_response_model.dart';
import 'package:studenda_mobile/feature/auth/domain/repositories/auth_repository.dart';


class AuthRepositoryImpl implements AuthRepository{

  final AuthRemoteDataSource remoteDataSource;
  final NetworkInfo networkInfo;

  AuthRepositoryImpl({required this.remoteDataSource, required this.networkInfo});

  @override
  Future<Either<Failure, SecurityResponseModel>> auth(SecurityRequestModel request) async {
    if( await networkInfo.isConnected ){
      try{
        final remoteAuth = await remoteDataSource.auth(request);
        //TODO: localdatasource cache
        return Right(remoteAuth);
      } on ServerException{
        return const Left(ServerFailure(message: "Ошибка сервера"));
      }
    } else{
      //TODO: get data from cache
    }
    return const Left(AuthFailure(message: "Ошибка авторизации"));
  }
  
}