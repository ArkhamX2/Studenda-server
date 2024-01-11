// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'security_request_model.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

_$SecurityRequestModelImpl _$$SecurityRequestModelImplFromJson(
        Map<String, dynamic> json) =>
    _$SecurityRequestModelImpl(
      email: json['email'] as String,
      password: json['password'] as String,
      roleName: json['roleName'] as String,
    );

Map<String, dynamic> _$$SecurityRequestModelImplToJson(
        _$SecurityRequestModelImpl instance) =>
    <String, dynamic>{
      'email': instance.email,
      'password': instance.password,
      'roleName': instance.roleName,
    };
