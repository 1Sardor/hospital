from django.contrib import admin
from django.urls import path, include
from drf_yasg.views import get_schema_view
from drf_yasg import openapi
from rest_framework import permissions
from api.router import router
from django.conf import settings
from django.conf.urls.static import static
from api.views import Login
from api.views import Pharmacylogin


schema_view = get_schema_view(
    openapi.Info(
          title="Snippets API",
          default_version='v1',
          description="Test description",
          terms_of_service="https://www.google.com/policies/terms/",
          contact=openapi.Contact(email="contact@snippets.local"),
          license=openapi.License(name="BSD License"),
    ),
    public=True,
    permission_classes=(permissions.AllowAny, ),
)

urlpatterns = [
    path('admin/', admin.site.urls),
    path('swagger/', schema_view.with_ui('swagger', cache_timeout=0), name='schema-swagger-ui'),
    path('api/', include(router.urls)),
    path('login/', Login),
    path('pharmacylogin/', Pharmacylogin),

]+static(settings.MEDIA_URL, document_root=settings.MEDIA_ROOT)