import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:studenda_mobile/core/data/error/exception.dart';
import 'package:studenda_mobile/feature/auth/data/models/security_request_model.dart';
import 'package:studenda_mobile/feature/auth/data/models/security_response_model.dart';

abstract class AuthRemoteDataSource {
  Future<SecurityResponseModel> auth(SecurityRequestModel request);
}

class AuthRemoteDataSourceImpl implements AuthRemoteDataSource {
  final http.Client client;

  AuthRemoteDataSourceImpl({required this.client});

  @override
  Future<SecurityResponseModel> auth(SecurityRequestModel request) async {
    final response = await client.post(
        Uri.parse('http://88.210.3.137/api/security/login'),
        body: request,
        headers: {
          'Content-Type': 'application/json',
        });

    if (response.statusCode == 200) {
      return SecurityResponseModel.fromJson(
          json.decode(response.body) as Map<String, dynamic>);
    } else {
      throw ServerException();
    }
  }
}
