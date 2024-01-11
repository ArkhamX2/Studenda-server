import 'package:connectivity_plus/connectivity_plus.dart';
import 'package:get_it/get_it.dart';
import 'package:http/http.dart' as http;
import 'package:studenda_mobile/core/network/network_info.dart';
import 'package:studenda_mobile/feature/auth/data/datasources/auth_remote_datasource.dart';
import 'package:studenda_mobile/feature/auth/data/repositories/auth_repository_impl.dart';
import 'package:studenda_mobile/feature/auth/domain/repositories/auth_repository.dart';
import 'package:studenda_mobile/feature/auth/domain/usecases/auth.dart';
import 'package:studenda_mobile/feature/auth/presentation/bloc/bloc/auth_bloc.dart';

final sl = GetIt.instance;

Future<void> init() async {
  //! Features
  // Bloc
  sl.registerFactory(
    () => AuthBloc(
      authUseCase: sl(),
    ),
  );

  // Use cases
  sl.registerLazySingleton(
    () => Auth(
      authRepository: sl(),
    ),
  );

  // Repository
  sl.registerLazySingleton<AuthRepository>(
    () => AuthRepositoryImpl(
      remoteDataSource: sl(),
      networkInfo: sl(),
    ),
  );

  //! Data sources

  sl.registerLazySingleton<AuthRemoteDataSource>(
    () => AuthRemoteDataSourceImpl(
      client: sl(),
    ),
  );

  //! Core

  sl.registerLazySingleton<NetworkInfo>(
    () => NetworkInfoImpl(
      sl(),
    ),
  );

  //! External

  // final sharedPreferences = await SgaredPreferences.getInstance();
  // sl.registerLazySingleton(() => sharedPreferences);
  sl.registerLazySingleton(() => http.Client());
  sl.registerLazySingleton(() => Connectivity(),);
}
