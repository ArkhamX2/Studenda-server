import 'package:flutter/material.dart';

class MainAuthWidget extends StatefulWidget {
  const MainAuthWidget({super.key});

  @override
  State<MainAuthWidget> createState() => _MainAuthWidgetState();
}

class _MainAuthWidgetState extends State<MainAuthWidget> {
  final buttonTextStyle =
      const TextStyle(color: Color.fromARGB(255, 56, 31, 118), fontSize: 40);

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: const Color.fromARGB(255, 228, 225, 248),
      body: Container(
        alignment: AlignmentDirectional.center,
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Text(
              "СТУДЕНДА",
              style: buttonTextStyle,
            ),
            const SizedBox(
              height: 40,
            ),
            ElevatedButton(
              onPressed: () {},
              style: ButtonStyle(
                shape: MaterialStatePropertyAll(
                  RoundedRectangleBorder(
                    borderRadius: BorderRadius.circular(9),
                  ),
                ),
                backgroundColor: const MaterialStatePropertyAll(
                  Color.fromARGB(255, 56, 31, 118),
                ),
              ),
              child: const Text(
                "Войти",
                style: TextStyle(
                  color: Colors.white,
                  fontSize: 23,
                ),
              ),
            ),
            const SizedBox(
              height: 34,
            ),
            ElevatedButton(
              onPressed: () {},
              style: ButtonStyle(
                shape: MaterialStatePropertyAll(
                  RoundedRectangleBorder(
                    borderRadius: BorderRadius.circular(9),
                  ),
                ),
                backgroundColor: const MaterialStatePropertyAll(
                  Colors.transparent,
                ),
                elevation: const MaterialStatePropertyAll(0),
              ),
              child: const Text(
                "Войти как гость",
                style: TextStyle(
                    color: Color.fromARGB(255, 56, 31, 118), fontSize: 20),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
