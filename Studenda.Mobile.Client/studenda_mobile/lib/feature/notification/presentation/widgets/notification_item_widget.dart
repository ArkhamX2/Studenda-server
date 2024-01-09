import 'package:flutter/material.dart';
import 'package:studenda_mobile/model/common/notification.dart';
import 'package:studenda_mobile/resources/colors.dart';

class NotificationItemWidget extends StatelessWidget {
  final StudendaNotification notification;

  const NotificationItemWidget({super.key, required this.notification});

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.symmetric(vertical: 5),
      child: Container(
        decoration: BoxDecoration(
          border: Border.all(
            color: Colors.white,
          ),
          color: Colors.white,
          borderRadius: const BorderRadius.all(Radius.circular(5)),
        ),
        child: Padding(
          padding: const EdgeInsets.all(10.0),
          child: Row(
            children: [
              Expanded(
                child: Text(
                  notification.title,
                  style: const TextStyle(
                    color: mainForegroundColor,
                    fontSize: 16,
                  ),
                ),
              ),
              Text(
                notification.date,
                style: const TextStyle(
                  color: mainForegroundColor,
                  fontSize: 16,
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
