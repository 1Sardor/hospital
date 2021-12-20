from rest_framework.decorators import action, api_view
from django.contrib.auth import authenticate
from rest_framework.authentication import TokenAuthentication
from rest_framework.authtoken.models import Token
from rest_framework.response import Response
from rest_framework import viewsets
from .serializer import *
from main.models import *
from rest_framework.permissions import IsAuthenticated
from django.db.models import Q


@api_view(['POST'])
def Login(request):
    try:
        username = request.data['username']
        password = request.data['password']
        try:
            usrs = Doctor.objects.get(username=username)
            user = authenticate(username=username, password=password)
            if user is not None:
                status = 200
                token, created = Token.objects.get_or_create(user=user)
                data = {
                    'status': status,
                    'username': username,
                    'user_id': usrs.id,
                    'token': token.key,
                    'phone': usrs.phone,
                    'direction': usrs.direction.name,
                    'hospital': usrs.hospital.name,
                    'hospital type': usrs.hospital.type,
                }
            else:
                status = 403
                message = 'Username yoki parol xato!'
                data = {
                    'status': status,
                    'message': message,
                }
        except Doctor.DoesNotExist:
            status = 404
            message = 'Bunday foydalanuvchi mavjud emas!'
            data = {
                'status': status,
                'message': message,
            }
        return Response(data)
    except Exception as er:
        return Response({"error": f'{er}'})


class DoctorView(viewsets.ModelViewSet):
    queryset = Doctor.objects.all()
    serializer_class = DoctorSerializer
    authentication_classes = (TokenAuthentication,)
    permission_classes = (IsAuthenticated,)

    def create(self, request):
        try:
            username = request.data['username']
            password = request.data['password']
            direction_id = request.data['direction']
            hospital_id = request.data['hospital']
            phone = request.data['phone']
            user = Doctor.objects.create_user(username=username, password=password, phone=phone, direction_id=direction_id, hospital_id=hospital_id,)
            ser = self.serializer_class(user)
            return Response(ser.data)
        except Exception as err:
            return Response({'error': f'{err}'})

    @action(methods=['GET'], detail=False)
    def get_hospital_doctors(self, request):
        try:
            hospital_id = request.data['hospital']
            query = self.queryset.filter(hospital_id=hospital_id)
            ser = self.serializer_class(query, many=True)
            return Response(ser.data)
        except Exception as err:
            return Response({'error': f'{err}'})


class DirectionView(viewsets.ModelViewSet):
    queryset = Direction.objects.all()
    serializer_class = DirectionSerializer
    authentication_classes = (TokenAuthentication,)
    permission_classes = (IsAuthenticated,)


class ProvinceView(viewsets.ModelViewSet):
    queryset = Province.objects.all()
    serializer_class = ProvinceSerializer
    authentication_classes = (TokenAuthentication,)
    permission_classes = (IsAuthenticated,)


class RegionView(viewsets.ModelViewSet):
    queryset = Region.objects.all()
    serializer_class = RegionSerializer
    authentication_classes = (TokenAuthentication,)
    permission_classes = (IsAuthenticated,)

    @action(methods=['GET'], detail=False)
    def get_province_region(self, request):
        try:
            province_id = request.data['province']
            query = self.queryset.filter(province_id=province_id)
            ser = self.serializer_class(query, many=True)
            return Response(ser.data)
        except Exception as err:
            return Response({'error': f'{err}'})


class HospitalView(viewsets.ModelViewSet):
    queryset = Hospital.objects.all()
    serializer_class = HospitalSerializer
    authentication_classes = (TokenAuthentication,)
    permission_classes = (IsAuthenticated,)


class PatientView(viewsets.ModelViewSet):
    queryset = Patient.objects.all()
    serializer_class = PatientSerializer
    authentication_classes = (TokenAuthentication,)
    permission_classes = (IsAuthenticated,)

    @action(methods=['GET'], detail=False)
    def search(self, request):
        try:
            search = request.GET['search']
            query = self.queryset.filter(name__icontains=search)
            ser = self.serializer_class(query, many=True)
            return Response(ser.data)
        except Exception as err:
            return Response({'error': f'{err}'})


class RetseptsView(viewsets.ModelViewSet):
    queryset = Retsepts.objects.all()
    serializer_class = RetseptsSerializer
    authentication_classes = (TokenAuthentication,)
    permission_classes = (IsAuthenticated,)

    @action(methods=['GET'], detail=False)
    def get_retsepts_of_doctor(self, request):
        try:
            user = request.user
            query = self.queryset.filter(doctor=user)
            ser = self.serializer_class(query, many=True)
            return Response(ser.data)
        except Exception as err:
            return Response({'error': f'{err}'})

    @action(methods=['GET'], detail=False)
    def get_retsepts_of_client(self, request):
        try:
            client = request.GET['client']
            query = self.queryset.filter(client=client)
            ser = self.serializer_class(query, many=True)
            return Response(ser.data)
        except Exception as err:
            return Response({'error': f'{err}'})


class CommentsView(viewsets.ModelViewSet):
    queryset = Comments.objects.all()
    serializer_class = CommentsSerializer
    authentication_classes = (TokenAuthentication,)
    permission_classes = (IsAuthenticated,)

    def create(self, request):
        try:
            patient_id = request.data['patient']
            doctor_id = request.data['doctor']
            ill = request.data['ill']
            com = Comments.objects.create(patient_id=patient_id, doctor_id=doctor_id, ill=ill)
            ser = self.serializer_class(com)
            return Response(ser.data)
        except Exception as err:
            return Response({'error': f'{err}'})

    @action(methods=['GET'], detail=False)
    def get_comment_of_client(self, request):
        try:
            client = request.GET['client']
            query = self.queryset.filter(client=client)
            ser = self.serializer_class(query, many=True)
            return Response(ser.data)
        except Exception as err:
            return Response({'error': f'{err}'})

    @action(methods=['GET'], detail=False)
    def get_comment_of_doctor(self, request):
        try:
            user = request.user
            query = self.queryset.filter(doctor=user)
            ser = self.serializer_class(query, many=True)
            return Response(ser.data)
        except Exception as err:
            return Response({'error': f'{err}'})


class DrugsView(viewsets.ModelViewSet):
    queryset = Drugs.objects.all()
    serializer_class = DrugsSerializer
    authentication_classes = (TokenAuthentication,)
    permission_classes = (IsAuthenticated,)

